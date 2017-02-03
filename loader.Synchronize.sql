USE [erbd_gia_reg_17_70]
GO

/****** Object:  StoredProcedure [loader].[Synchronize]    Script Date: 03.02.2017 18:10:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ======================================================
-- Автор:          Блюдов А.В.
-- Дата создания:  30.01.17 17:35
-- Дата изменения: 01.01.17 19:10 Добавлена синхронизация таблиц участников ГИА
-- Дата изменения: 02.01.17 12:15 Добавлена синхронизация таблиц для работников ППЭ
-- Описание:       Процедура синхронизации данных
-- ======================================================
-- Параметры:
-- @TableGroup        - группа таблиц
-- @SkipErrors        - пропускать ошибки загрузки
-- Коды ошибок при импорте данных:
CREATE PROCEDURE [loader].[Synchronize]
        @TableGroup SMALLINT,
        @SkipErrors BIT = 0,
        @error_count INT = 10
 WITH RECOMPILE
AS
BEGIN
    DECLARE @rows_count INT = 0,
            @nwd_count INT = 0,             -- количество записей с новыми данными
            @start_date DATETIME2,
            @task_start_date DATETIME2,
            @end_date DATETIME2,
            @elapsed INT = 0,
            @info NVARCHAR(MAX),            -- информационное сообщение
            @crow INT,                      -- переменная для итерация в курсоре
            @errors_show INT = @error_count

    SET @start_date = GETDATE()
    SET @task_start_date = GETDATE()
    SET @end_date = @start_date
    
    SET NOCOUNT ON

    --BEGIN TRANSACTION importing
    BEGIN TRY
        SET @start_date = GETDATE()

        IF EXISTS(SELECT lcr.ID
                    FROM loader.rbd_CurrentRegion AS lcr
                   WHERE NOT EXISTS(SELECT dcr.ID FROM dbo.rbd_CurrentRegion AS dcr
                                     WHERE dcr.ID = lcr.ID))
        BEGIN
            RAISERROR ('Загрузка информации о субъекте РФ...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_CurrentRegion AS tgt
            USING loader.rbd_CurrentRegion AS src
               ON src.REGION = tgt.REGION AND
                  src.ID = tgt.ID
            WHEN NOT MATCHED
            THEN INSERT (ID, REGION, Name, RCOIName, RCOILawAddress, RCOIAddress, RCOIProperty,
                         RCOIDPosition, RCOIDFio, RCOIPhones, RCOIFaxs, RCOIEMails, GEKAddress,
                         GEKDFio, GEKPhones, GEKFaxs, GEKEMails, EGEWWW)
            VALUES (src.ID, src.REGION, src.Name, src.RCOIName, src.RCOILawAddress, src.RCOIAddress, src.RCOIProperty,
                    src.RCOIDPosition, src.RCOIDFio, src.RCOIPhones, src.RCOIFaxs, src.RCOIEMails, src.GEKAddress,
                    src.GEKDFio, src.GEKPhones, src.GEKFaxs, src.GEKEMails, src.EGEWWW)
            WHEN MATCHED AND (tgt.Name <> src.Name OR
                              tgt.RCOIName <> src.RCOIName OR
                              tgt.RCOILawAddress <> src.RCOILawAddress OR
                              tgt.RCOIAddress <> src.RCOIAddress OR
                              tgt.RCOIProperty <> src.RCOIProperty OR
                              tgt.RCOIDPosition <> src.RCOIDPosition OR
                              tgt.RCOIDFio <> src.RCOIDFio OR
                              tgt.RCOIPhones <> src.RCOIPhones OR
                              tgt.RCOIFaxs <> src.RCOIFaxs OR
                              tgt.RCOIEMails <> src.RCOIEMails OR
                              tgt.GEKAddress <> src.GEKAddress OR
                              tgt.GEKDFio <> src.GEKDFio OR
                              tgt.GEKPhones <> src.GEKPhones OR
                              tgt.GEKFaxs <> src.GEKFaxs OR
                              tgt.GEKEMails <> src.GEKEMails OR
                              tgt.EGEWWW <> src.EGEWWW)
            THEN UPDATE SET Name = src.Name,
                            RCOIName = src.RCOIName,
                            RCOILawAddress = src.RCOILawAddress,
                            RCOIAddress = src.RCOIAddress,
                            RCOIProperty = src.RCOIProperty,
                            RCOIDPosition = src.RCOIDPosition,
                            RCOIDFio = src.RCOIDFio,
                            RCOIPhones = src.RCOIPhones,
                            RCOIFaxs = src.RCOIFaxs,
                            RCOIEMails = src.RCOIEMails,
                            GEKAddress = src.GEKAddress,
                            GEKDFio = src.GEKDFio,
                            GEKPhones = src.GEKPhones,
                            GEKFaxs = src.GEKFaxs,
                            GEKEMails = src.GEKEMails,
                            EGEWWW = src.EGEWWW
            WHEN NOT MATCHED BY SOURCE
            THEN DELETE;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка информации о субъекте РФ (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о субъекте РФ...', 0, 0) WITH NOWAIT;

        IF EXISTS(SELECT src.AreaID
                    FROM loader.rbd_Areas AS src
                   WHERE NOT EXISTS(SELECT tgt.AreaID FROM dbo.rbd_Areas AS tgt
                                     WHERE src.AreaID = tgt.AreaID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка районов...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_Areas AS tgt
            USING loader.rbd_Areas AS src
               ON src.AreaID = tgt.AreaID
            WHEN NOT MATCHED
            THEN INSERT (AreaID, Region, AreaCode, AreaName, LawAddress, [Address],
                         ChargeFIO, Phones, Mails, WWW, IsDeleted)
            VALUES (src.AreaID, src.Region, src.AreaCode, src.AreaName, src.LawAddress, src.[Address],
                    src.ChargeFIO, src.Phones, src.Mails, src.WWW, src.IsDeleted)
            WHEN MATCHED AND (src.Region = tgt.Region OR
                              src.AreaCode <> tgt.AreaCode OR
                              src.AreaName <> tgt.AreaName OR
                              src.LawAddress <> tgt.LawAddress OR
                              src.[Address] <> tgt.[Address] OR
                              src.ChargeFIO <> tgt.ChargeFIO OR
                              src.Phones <> tgt.Phones OR
                              src.Mails <> tgt.Mails OR
                              src.WWW <> tgt.WWW OR
                              src.IsDeleted <> tgt.IsDeleted)
            THEN UPDATE SET Region = src.Region,
                            AreaCode = src.AreaCode,
                            AreaName = src.AreaName,
                            LawAddress = src.LawAddress,
                            [Address] = src.[Address],
                            ChargeFIO = src.ChargeFIO,
                            Phones = src.Phones,
                            Mails = src.Mails,
                            WWW = src.WWW,
                            IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка районов (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о районах субъекта РФ...', 0, 0) WITH NOWAIT;

        -- Загрузка адресов
        IF EXISTS(SELECT src.AddressID
                    FROM loader.rbd_Address AS src
                   WHERE NOT EXISTS(SELECT tgt.AddressID FROM dbo.rbd_Address AS tgt
                                     WHERE src.AddressID = tgt.AddressID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка адресов...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_Address AS tgt
            USING (SELECT AddressID, REGION, ZipCode, LocalityTypeID, LocalityName, StreetTypeID,
                          StreetName, BuildingTypeID, BuildingNumber, TownshipID, CreateDate,
                          UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted
                     FROM loader.rbd_Address AS a
                    WHERE EXISTS(SELECT REGION
                                   FROM dbo.rbdc_Regions AS r
                                  WHERE r.REGION = a.REGION) AND
                          EXISTS(SELECT lt.LocalityTypeID
                                   FROM dbo.rbdc_LocalityTypes AS lt
                                  WHERE lt.LocalityTypeID = a.LocalityTypeID) AND
                          EXISTS(SELECT st.StreetTypeID
                                   FROM dbo.rbdc_StreetTypes AS st
                                  WHERE st.StreetTypeID = a.StreetTypeID) AND
                          EXISTS(SELECT bt.BuildingTypeID
                                   FROM dbo.rbdc_BuildingTypes AS bt
                                  WHERE bt.BuildingTypeID = a.BuildingTypeID) AND
                          EXISTS(SELECT ts.TownshipID
                                   FROM dbo.rbd_Townships AS ts
                                  WHERE ts.TownshipID = a.TownshipID)

                ) AS src
               ON src.AddressID = tgt.AddressID
            WHEN NOT MATCHED
            THEN INSERT (AddressID, REGION, ZipCode, LocalityTypeID, LocalityName, StreetTypeID,
                         StreetName, BuildingTypeID, BuildingNumber, TownshipID, CreateDate,
                         UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted)
            VALUES (src.AddressID, src.REGION, src.ZipCode, src.LocalityTypeID, src.LocalityName, src.StreetTypeID,
                    src.StreetName, src.BuildingTypeID, src.BuildingNumber, src.TownshipID, src.CreateDate,
                    src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate, src.IsDeleted)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.ZipCode <> src.ZipCode OR
                              tgt.LocalityTypeID <> src.LocalityTypeID OR
                              tgt.LocalityName <> src.LocalityName OR
                              tgt.StreetTypeID <> src.StreetTypeID OR
                              tgt.StreetName <> src.StreetName OR
                              tgt.BuildingTypeID <> src.BuildingTypeID OR
                              tgt.BuildingNumber <> src.BuildingNumber OR
                              tgt.TownshipID <> src.TownshipID OR
                              tgt.IsDeleted <> src.IsDeleted)
            THEN UPDATE SET REGION = src.REGION,
                            ZipCode = src.ZipCode,
                            LocalityTypeID = src.LocalityTypeID,
                            LocalityName = src.LocalityName,
                            StreetTypeID = src.StreetTypeID,
                            StreetName = src.StreetName,
                            BuildingTypeID = src.BuildingTypeID,
                            BuildingNumber = src.BuildingNumber,
                            TownshipID = src.TownshipID,
                            UpdateDate = GETDATE(),
                            ImportUpdateDate = GETDATE(),
                            IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;
            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка адресов субъекта РФ (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных об адресах субъекта РФ...', 0, 0) WITH NOWAIT;

        -- Загрузка адресов для РЦОИ
        IF EXISTS(SELECT src.CurrentRegionAddressID
                    FROM loader.rbd_CurrentRegionAddress AS src
                   WHERE NOT EXISTS(SELECT tgt.CurrentRegionAddressID FROM dbo.rbd_CurrentRegionAddress AS tgt
                                     WHERE src.CurrentRegionAddressID = tgt.CurrentRegionAddressID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка адресов для РЦОИ...', 0, 0) WITH NOWAIT;
            DECLARE ccur INSENSITIVE CURSOR
                FOR SELECT 'Для адреса РЦОИ ID: {' + CAST(CurrentRegionAddressID AS VARCHAR(40)) + '}' +
                           CASE WHEN crg.REGION IS NULL THEN ' отсутствует запись в rbd_CurrentRegion' ELSE '' END +
                           CASE WHEN adr.AddressID IS NULL THEN ' отсутствует запись в rbd_Address' ELSE '' END +
                           CASE WHEN atp.AddressTypeID IS NULL THEN ' отсутствует запись в rbdc_AddressTypes' ELSE '' END
                      FROM loader.rbd_CurrentRegionAddress AS cra
                     OUTER APPLY(SELECT REGION
                                   FROM dbo.rbd_CurrentRegion AS cr
                                  WHERE cr.ID = cra.CurrentRegionID AND
                                        cr.REGION = cra.REGION) AS crg
                     OUTER APPLY (SELECT a.AddressID
                                    FROM dbo.rbd_Address AS a
                                   WHERE a.AddressID = cra.AddressID) AS adr
                     OUTER APPLY(SELECT at.AddressTypeID
                                   FROM dbo.rbdc_AddressTypes AS at
                                  WHERE at.AddressTypeID = cra.AddressTypeID) AS atp
                    WHERE crg.REGION IS NULL OR
                          adr.AddressID IS NULL OR
                          atp.AddressTypeID IS NULL
            OPEN ccur
            FETCH NEXT FROM ccur
             INTO @info
            WHILE @@FETCH_STATUS = 0
            BEGIN
        
                RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                FETCH NEXT FROM ccur
                 INTO @info
            END
            CLOSE ccur
            DEALLOCATE ccur

            MERGE dbo.rbd_CurrentRegionAddress AS tgt
            USING (SELECT cra.CurrentRegionAddressID, REGION, CurrentRegionID, AddressID, AddressTypeID,
                          AddressKind, CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted
                     FROM loader.rbd_CurrentRegionAddress AS cra
                     WHERE EXISTS(SELECT REGION
                                   FROM dbo.rbd_CurrentRegion AS cr
                                  WHERE cr.ID = cra.CurrentRegionID AND
                                        cr.REGION = cra.REGION) AND
                           EXISTS(SELECT a.AddressID
                                    FROM dbo.rbd_Address AS a
                                   WHERE a.AddressID = cra.AddressID) AND
                           EXISTS(SELECT at.AddressTypeID
                                    FROM dbo.rbdc_AddressTypes AS at
                                   WHERE at.AddressTypeID = cra.AddressTypeID)
                     )
               AS src
               ON src.CurrentRegionAddressID = tgt.CurrentRegionAddressID
            WHEN NOT MATCHED
            THEN INSERT (CurrentRegionAddressID, REGION, CurrentRegionID, AddressID, AddressTypeID,
                         AddressKind, CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted)
            VALUES (src.CurrentRegionAddressID, src.REGION, src.CurrentRegionID, src.AddressID,
                    src.AddressTypeID, src.AddressKind, src.CreateDate, src.UpdateDate,
                    src.ImportCreateDate, src.ImportUpdateDate, src.IsDeleted)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.CurrentRegionID <> src.CurrentRegionID OR
                              tgt.AddressID <> src.AddressID OR
                              tgt.AddressTypeID <> src.AddressTypeID OR
                              tgt.AddressKind <> src.AddressKind OR
                              --tgt.CreateDate <> src.CreateDate OR
                              --tgt.UpdateDate <> src.UpdateDate OR
                              --tgt.ImportCreateDate <> src.ImportCreateDate OR
                              --tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.IsDeleted <> src.IsDeleted)
            THEN UPDATE SET REGION = src.REGION,
                            CurrentRegionID = src.CurrentRegionID,
                            AddressID = src.AddressID,
                            AddressTypeID = src.AddressTypeID,
                            AddressKind = src.AddressKind,
                            --CreateDate = src.CreateDate,
                            UpdateDate = GETDATE(),
                            --ImportCreateDate = src.ImportCreateDate,
                            ImportUpdateDate = GETDATE(),
                            IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка адресов для РЦОИ (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных об адресах РЦОИ...', 0, 0) WITH NOWAIT;

        -- Загрузка муниципалитетов
        IF EXISTS(SELECT src.GovernmentID
                    FROM loader.rbd_Governments AS src
                   WHERE NOT EXISTS(SELECT tgt.GovernmentID FROM dbo.rbd_Governments AS tgt
                                     WHERE src.GovernmentID = tgt.GovernmentID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка МОУО...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_Governments AS tgt
            USING (SELECT g.GovernmentID, cr.ID AS RegionID, g.REGION, g.GovernmentCode, g.GovernmentName,
                          g.LawAddress, g.[Address], g.ChargeFIO, g.Phones, g.Mails, g.WWW, g.ChargePosition, g.Faxes, g.GType, g.SpecialistFIO,
                          g.SpecialistMails, g.SpecialistPhones, g.DeleteType, g.CreateDate, g.UpdateDate, g.ImportCreateDate, g.ImportUpdateDate,
                          g.TimeZoneId
                     FROM loader.rbd_Governments AS g
                     JOIN rbd_CurrentRegion AS cr
                       ON g.REGION = cr.REGION)
               AS src
               ON src.GovernmentID = tgt.GovernmentID
            WHEN NOT MATCHED
            THEN INSERT (GovernmentID, RegionID, REGION, GovernmentCode, GovernmentName, LawAddress, [Address],
                         ChargeFIO, Phones, Mails, WWW, ChargePosition, Faxes, GType, SpecialistFIO, SpecialistMails,
                         SpecialistPhones, DeleteType, CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate,
                         TimeZoneId)
            VALUES (src.GovernmentID, src.RegionID, src.REGION, src.GovernmentCode, src.GovernmentName, src.LawAddress, src.[Address],
                    src.ChargeFIO, src.Phones, src.Mails, src.WWW, src.ChargePosition, src.Faxes, src.GType, src.SpecialistFIO, src.SpecialistMails,
                    src.SpecialistPhones, src.DeleteType, src.CreateDate, src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate,
                    src.TimeZoneId)
            WHEN MATCHED AND (src.GovernmentCode <> tgt.GovernmentCode OR
                              src.GovernmentName <> tgt.GovernmentName OR
                              src.LawAddress <> tgt.LawAddress OR
                              src.[Address] <> tgt.[Address] OR
                              src.ChargeFIO <> tgt.ChargeFIO OR
                              src.Phones <> tgt.Phones OR
                              src.Mails <> tgt.Mails OR
                              src.WWW <> tgt.WWW OR
                              src.ChargePosition <> tgt.ChargePosition OR
                              src.Faxes <> tgt.Faxes OR
                              src.GType <> tgt.GType OR
                              src.SpecialistFIO <> tgt.SpecialistFIO OR
                              src.SpecialistMails <> tgt.SpecialistMails OR
                              src.SpecialistPhones <> tgt.SpecialistPhones OR
                              src.DeleteType <> tgt.DeleteType)
            THEN UPDATE SET GovernmentCode = src.GovernmentCode,
                            GovernmentName = src.GovernmentName,
                            LawAddress = src.LawAddress,
                            [Address] = src.[Address],
                            ChargeFIO = src.ChargeFIO,
                            Phones = src.Phones,
                            Mails = src.Mails,
                            WWW = src.WWW,
                            ChargePosition = src.ChargePosition,
                            Faxes = src.Faxes,
                            GType = src.GType,
                            SpecialistFIO = src.SpecialistFIO,
                            SpecialistMails = src.SpecialistMails,
                            SpecialistPhones = src.SpecialistPhones,
                            DeleteType = src.DeleteType,
                            UpdateDate = GETDATE(),
                            ImportUpdateDate = GETDATE()
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET DeleteType = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка МОУО (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о МОУО...', 0, 0) WITH NOWAIT;

        -- Загрузка населённых пунктов
        IF EXISTS(SELECT src.TownshipID
                    FROM loader.rbd_Townships AS src
                   WHERE NOT EXISTS(SELECT tgt.TownshipID FROM dbo.rbd_Townships AS tgt
                                     WHERE src.TownshipID = tgt.TownshipID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка населённых пунктов...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_Townships AS tgt
            USING (SELECT ts.TownshipID, ts.REGION, ts.TownshipName, ts.OCATO, ts.TimeZoneID
                     FROM loader.rbd_Townships AS ts
                     JOIN dbo.rbdc_TimeZones AS tz
                       ON ts.TimeZoneID = tz.TimeZoneID)
               AS src
               ON src.Region = tgt.Region AND
                  src.TownshipID = tgt.TownshipID
            WHEN NOT MATCHED
            THEN INSERT (TownshipID, REGION, TownshipName, OCATO, TimeZoneID)
            VALUES (src.TownshipID, src.REGION, src.TownshipName, src.OCATO, src.TimeZoneID)
            WHEN MATCHED AND (src.TownshipName <> tgt.TownshipName OR
                              src.OCATO <> tgt.OCATO OR
                              src.TimeZoneID <> tgt.TimeZoneID)
            THEN UPDATE SET TownshipName = src.TownshipName, OCATO = src.OCATO, TimeZoneID = src.TimeZoneID;
            --WHEN NOT MATCHED BY SOURCE THEN DELETE;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка населённых пунктов (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о населённых пунктах...', 0, 0) WITH NOWAIT;

        -- Загрузка школ
        IF EXISTS(SELECT src.SchoolID
                    FROM loader.rbd_Schools AS src
                   WHERE NOT EXISTS(SELECT tgt.SchoolID FROM dbo.rbd_Schools AS tgt
                                     WHERE src.SchoolID = tgt.SchoolID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка ОО...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_Schools AS tgt
            USING (SELECT sl.*
                     FROM loader.rbd_Schools AS sl
                     WHERE EXISTS(SELECT GovernmentID
                                    FROM dbo.rbd_Governments AS g
                                   WHERE g.GovernmentID = sl.GovernmentID) AND
                           EXISTS(SELECT AreaID
                                    FROM dbo.rbd_Areas AS a
                                   WHERE a.AreaID = sl.AreaFK) AND
                           EXISTS(SELECT SchoolKindID FROM dbo.rbdc_SchoolKinds AS sk WHERE sk.SchoolKindID = sl.SchoolKindFK) AND
                           --EXISTS(SELECT SchoolTypeID FROM dbo.rbdc_SchoolTypes AS st WHERE st.SchoolTypeID = sl.SchoolTypeFK) AND
                           EXISTS(SELECT SchoolPropertyID FROM dbo.rbdc_SchoolProperties AS sp WHERE sp.SchoolPropertyID = sl.SchoolPropertyFK) AND
                           EXISTS(SELECT TownshipID FROM dbo.rbd_Townships AS t WHERE t.TownshipID = sl.TownshipFK)
                     )
               AS src
               ON src.SchoolID = tgt.SchoolID
            WHEN NOT MATCHED
            THEN INSERT (SchoolID, GovernmentID, SchoolCode, SchoolName, SchoolKindFK, SchoolPropertyFk, AreaFK, TownTypeFK,
                         LawAddress, [Address], DPosition, FIO, Phones, Faxs, Mails, People11, People9, ChargeFIO, WWW, LicNum,
                         LicRegNo, LicIssueDate, LicFinishingDate, AccCertNum, AccCertRegNo, AccCertIssueDate, AccCertFinishingDate,
                         isVirtualSchool, SReserve1, SReserve2, DeleteType, IsTOM, REGION, ShortName, TownshipFK, CreateDate, UpdateDate,
                         ImportCreateDate, ImportUpdateDate, SchoolFlags)
            VALUES (src.SchoolID, src.GovernmentID, src.SchoolCode, src.SchoolName, src.SchoolKindFK, src.SchoolPropertyFk, src.AreaFK,
                    src.TownTypeFK, src.LawAddress, src.[Address], src.DPosition, src.FIO, src.Phones, src.Faxs, src.Mails, src.People11,
                    src.People9, src.ChargeFIO, src.WWW, src.LicNum, src.LicRegNo, src.LicIssueDate, src.LicFinishingDate, src.AccCertNum,
                    src.AccCertRegNo, src.AccCertIssueDate, src.AccCertFinishingDate, src.isVirtualSchool, src.SReserve1, src.SReserve2,
                    src.DeleteType, src.IsTOM, src.REGION, src.ShortName, src.TownshipFK, src.CreateDate, src.UpdateDate,
                    src.ImportCreateDate, src.ImportUpdateDate, src.SchoolFlags)
            WHEN MATCHED AND (src.GovernmentID <> tgt.GovernmentID OR
                              src.SchoolCode <> tgt.SchoolCode OR
                              src.SchoolName <> tgt.SchoolName OR
                              src.SchoolKindFK <> tgt.SchoolKindFK OR
                              src.SchoolPropertyFk <> tgt.SchoolPropertyFk OR
                              src.AreaFK <> tgt.AreaFK OR
                              src.TownTypeFK <> tgt.TownTypeFK OR
                              src.LawAddress <> tgt.LawAddress OR
                              src.[Address] <> tgt.[Address] OR
                              src.DPosition <> tgt.DPosition OR
                              src.FIO <> tgt.FIO OR
                              src.Phones <> tgt.Phones OR
                              src.Faxs <> tgt.Faxs OR
                              src.Mails <> tgt.Mails OR
                              src.People11 <> tgt.People11 OR
                              src.People9 <> tgt.People9 OR
                              src.ChargeFIO <> tgt.ChargeFIO OR
                              src.WWW <> tgt.WWW OR
                              src.LicNum <> tgt.LicNum OR
                              src.LicRegNo <> tgt.LicRegNo OR
                              src.LicIssueDate <> tgt.LicIssueDate OR
                              src.LicFinishingDate <> tgt.LicFinishingDate OR
                              src.AccCertNum <> tgt.AccCertNum OR
                              src.AccCertRegNo <> tgt.AccCertRegNo OR
                              src.AccCertIssueDate <> tgt.AccCertIssueDate OR
                              src.AccCertFinishingDate <> tgt.AccCertFinishingDate OR
                              src.isVirtualSchool <> tgt.isVirtualSchool OR
                              src.SReserve1 <> tgt.SReserve1 OR
                              src.SReserve2 <> tgt.SReserve2 OR
                              src.DeleteType <> tgt.DeleteType OR
                              src.IsTOM <> tgt.IsTOM OR
                              src.REGION <> tgt.REGION OR
                              src.ShortName <> tgt.ShortName OR
                              src.TownshipFK <> tgt.TownshipFK OR
                              --src.CreateDate <> tgt.CreateDate OR
                              --src.UpdateDate <> tgt.UpdateDate OR
                              --src.ImportCreateDate <> tgt.ImportCreateDate OR
                              --src.ImportUpdateDate <> tgt.ImportUpdateDate OR
                              src.SchoolFlags <> tgt.SchoolFlags)
                THEN UPDATE SET GovernmentID = src.GovernmentID,
                            SchoolCode = src.SchoolCode,
                            SchoolName = src.SchoolName,
                            SchoolKindFK = src.SchoolKindFK,
                            SchoolPropertyFk = src.SchoolPropertyFk,
                            AreaFK = src.AreaFK,
                            TownTypeFK = src.TownTypeFK,
                            LawAddress = src.LawAddress,
                            [Address] = src.[Address],
                            DPosition = src.DPosition,
                            FIO = src.FIO,
                            Phones = src.Phones,
                            Faxs = src.Faxs,
                            Mails = src.Mails,
                            People11 = src.People11,
                            People9 = src.People9,
                            ChargeFIO = src.ChargeFIO,
                            WWW = src.WWW,
                            LicNum = src.LicNum,
                            LicRegNo = src.LicRegNo,
                            LicIssueDate = src.LicIssueDate,
                            LicFinishingDate = src.LicFinishingDate,
                            AccCertNum = src.AccCertNum,
                            AccCertRegNo = src.AccCertRegNo,
                            AccCertIssueDate = src.AccCertIssueDate,
                            AccCertFinishingDate = src.AccCertFinishingDate,
                            isVirtualSchool = src.isVirtualSchool,
                            SReserve1 = src.SReserve1,
                            SReserve2 = src.SReserve2,
                            DeleteType = src.DeleteType,
                            IsTOM = src.IsTOM,
                            REGION = src.REGION,
                            ShortName = src.ShortName,
                            TownshipFK = src.TownshipFK,
                            UpdateDate = GETDATE(),
                            ImportCreateDate = src.ImportCreateDate,
                            ImportUpdateDate = GETDATE(),
                            SchoolFlags = src.SchoolFlags
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET DeleteType = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка ОО (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных об ОО...', 0, 0) WITH NOWAIT;

        -- Загрузка адресов школ
        IF EXISTS(SELECT src.SchoolAddressID
                    FROM loader.rbd_SchoolAddress AS src
                   WHERE NOT EXISTS(SELECT tgt.SchoolAddressID FROM dbo.rbd_SchoolAddress AS tgt
                                     WHERE src.SchoolAddressID = tgt.SchoolAddressID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка адресов ОО...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_SchoolAddress AS tgt
            USING (SELECT sa.*
                     FROM loader.rbd_SchoolAddress AS sa
                     WHERE EXISTS(SELECT REGION
                                   FROM dbo.rbdc_Regions AS r
                                  WHERE r.REGION = sa.REGION) AND
                           EXISTS(SELECT s.SchoolID
                                    FROM dbo.rbd_Schools AS s
                                   WHERE s.SchoolID = sa.SchoolID) AND
                           EXISTS(SELECT a.AddressID
                                    FROM dbo.rbd_Address AS a
                                   WHERE a.AddressID = sa.AddressID) AND
                           EXISTS(SELECT at.AddressTypeID
                                    FROM dbo.rbdc_AddressTypes AS at
                                   WHERE at.AddressTypeID = sa.AddressTypeID)
                     )
               AS src
               ON src.SchoolAddressID = tgt.SchoolAddressID
            WHEN NOT MATCHED
            THEN INSERT (SchoolAddressID, REGION, SchoolID, AddressID, AddressTypeID,
                         AddressKind, CreateDate, UpdateDate, ImportCreateDate,
                         ImportUpdateDate, IsDeleted)
            VALUES (src.SchoolAddressID, src.REGION, src.SchoolID, src.AddressID, src.AddressTypeID,
                    src.AddressKind, src.CreateDate, src.UpdateDate, src.ImportCreateDate,
                    src.ImportUpdateDate, src.IsDeleted)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.SchoolID <> src.SchoolID OR
                              tgt.AddressID <> src.AddressID OR
                              tgt.AddressTypeID <> src.AddressTypeID OR
                              tgt.AddressKind <> src.AddressKind OR
                              --tgt.CreateDate <> src.CreateDate OR
                              --tgt.UpdateDate <> src.UpdateDate OR
                              --tgt.ImportCreateDate <> src.ImportCreateDate OR
                              --tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.IsDeleted <> src.IsDeleted)
            THEN UPDATE SET REGION = src.REGION,
                            SchoolID = src.SchoolID,
                            AddressID = src.AddressID,
                            AddressTypeID = src.AddressTypeID,
                            AddressKind = src.AddressKind,
                            --CreateDate = src.CreateDate,
                            UpdateDate = GETDATE(),
                            --ImportCreateDate = src.ImportCreateDate,
                            ImportUpdateDate = GETDATE(),
                            IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка адресов ОО (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных об адресах ОО...', 0, 0) WITH NOWAIT;
        --===============================================================================--
        -- Загрузка ППЭ
        SELECT @nwd_count = COUNT(src.StationID)
          FROM loader.rbd_Stations AS src
         WHERE NOT EXISTS(SELECT tgt.StationID FROM dbo.rbd_Stations AS tgt
                           WHERE src.StationID = tgt.StationID) OR
               EXISTS(SELECT StationID
                        FROM dbo.rbd_Stations AS tgt
                       WHERE src.StationID = tgt.StationID AND
                             (tgt.REGION <> src.REGION OR
                              tgt.AreaID <> src.AreaID OR
                              tgt.StationCode <> src.StationCode OR
                              tgt.StationName <> src.StationName OR
                              tgt.StationAddress <> src.StationAddress OR
                              tgt.SchoolFK <> src.SchoolFK OR
                              tgt.GovernmentID <> src.GovernmentID OR
                              tgt.sVolume <> src.sVolume OR
                              tgt.IsActive <> src.IsActive OR
                              tgt.Phones <> src.Phones OR
                              tgt.Mails <> src.Mails OR
                              tgt.PCenterID <> src.PCenterID OR
                              tgt.IsTOM <> src.IsTOM OR
                              tgt.DeleteType <> src.DeleteType OR
                              tgt.AuditoriumsCount <> src.AuditoriumsCount OR
                              tgt.ExamForm <> src.ExamForm OR
                              tgt.VideoControl <> src.VideoControl OR
                              tgt.AddressID <> src.AddressID OR
                              tgt.StationFlags <> src.StationFlags OR
                              tgt.TimeZoneId <> src.TimeZoneId))
        IF @nwd_count > 0
        BEGIN

            SET @rows_count = 0;
            SET @crow = 0;
            SELECT @rows_count = COUNT(st.StationID)
              FROM loader.rbd_Stations AS st
             WHERE NOT EXISTS(SELECT a.AreaID
                                FROM dbo.rbd_Areas AS a
                               WHERE a.AreaID = st.AreaID) OR
                   NOT EXISTS(SELECT g.GovernmentID
                                FROM dbo.rbd_Governments AS g
                               WHERE g.GovernmentID = st.GovernmentID)

            IF @rows_count > 0
            BEGIN
                DECLARE stcur INSENSITIVE CURSOR
                    FOR SELECT 'Для ППЭ ID: {' + CAST(s.StationID AS NVARCHAR(40)) + N'}:' +
                               CASE WHEN ar.AreaID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден район (' + CAST(ISNULL(s.AreaID, 'NULL') AS NVARCHAR(40)) + ')' ELSE N'' END +
                               CASE WHEN g.GovernmentID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден МОУО (' + CAST(ISNULL(s.GovernmentID, 'NULL') AS NVARCHAR(40)) + ')' ELSE N'' END
                         FROM loader.rbd_Stations AS s
                         OUTER APPLY (SELECT a.AreaID
                                       FROM dbo.rbd_Areas AS a
                                      WHERE a.AreaID = s.AreaID) AS ar
                         OUTER APPLY (SELECT g.GovernmentID
                                        FROM dbo.rbd_Governments AS g
                                       WHERE g.GovernmentID = s.GovernmentID) AS g
                         WHERE ar.AreaID IS NULL OR
                               g.GovernmentID IS NULL
                OPEN stcur
                FETCH NEXT FROM stcur
                 INTO @info
                WHILE @@FETCH_STATUS = 0 AND @crow < @errors_show
                BEGIN
                    RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                    FETCH NEXT FROM stcur
                     INTO @info
                    SET @crow = @crow + 1
                END
                CLOSE stcur
                DEALLOCATE stcur
                SET @rows_count = @rows_count - @errors_show
                RAISERROR (N'+ %d ошибок синхронизации ППЭ!', 8, 8, @rows_count ) WITH NOWAIT;
            END

            SET @crow = @nwd_count - @crow - @rows_count
            --RAISERROR ('ППЭ без ошибок: %d (%d)...', 0, 0, @crow, @nwd_count) WITH NOWAIT;
            IF @crow > 0
            BEGIN
                SET @start_date = GETDATE()
                RAISERROR ('Загрузка ППЭ...', 0, 0) WITH NOWAIT;

                MERGE dbo.rbd_Stations AS tgt
                USING (SELECT st.StationID, st.REGION, st.AreaID, st.StationCode, st.StationName, st.StationAddress,
                              st.SchoolFK, st.GovernmentID, st.sVolume, st.IsActive, st.Phones, st.Mails, st.PCenterID,
                              st.IsTOM, st.DeleteType, st.AuditoriumsCount, st.CreateDate, st.UpdateDate,  st.ImportCreateDate,
                              st.ImportUpdateDate, st.ExamForm, st.VideoControl, st.AddressID, st.StationFlags, st.TimeZoneId
                         FROM loader.rbd_Stations AS st
                         WHERE EXISTS(SELECT a.AreaID
                                        FROM dbo.rbd_Areas AS a
                                       WHERE a.AreaID = st.AreaID) AND
                               EXISTS(SELECT g.GovernmentID
                                        FROM dbo.rbd_Governments AS g
                                       WHERE g.GovernmentID = st.GovernmentID)
                         )
                   AS src
                   ON src.StationID = tgt.StationID
                WHEN NOT MATCHED
                THEN INSERT (StationID, REGION, AreaID, StationCode, StationName, StationAddress, SchoolFK,
                             GovernmentID, sVolume, IsActive, Phones, Mails, PCenterID, IsTOM, DeleteType,
                             AuditoriumsCount, CreateDate, UpdateDate,  ImportCreateDate, ImportUpdateDate,
                             ExamForm, VideoControl, AddressID, StationFlags, TimeZoneId)
                VALUES (src.StationID, src.REGION, src.AreaID, src.StationCode, src.StationName, src.StationAddress,
                        src.SchoolFK, src.GovernmentID, src.sVolume, src.IsActive, src.Phones, src.Mails, src.PCenterID,
                        src.IsTOM, src.DeleteType, src.AuditoriumsCount, src.CreateDate, src.UpdateDate, src.ImportCreateDate,
                        src.ImportUpdateDate, src.ExamForm, src.VideoControl, src.AddressID, src.StationFlags, src.TimeZoneId)
                WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                                  tgt.AreaID <> src.AreaID OR
                                  tgt.StationCode <> src.StationCode OR
                                  tgt.StationName <> src.StationName OR
                                  tgt.StationAddress <> src.StationAddress OR
                                  tgt.SchoolFK <> src.SchoolFK OR
                                  tgt.GovernmentID <> src.GovernmentID OR
                                  tgt.sVolume <> src.sVolume OR
                                  tgt.IsActive <> src.IsActive OR
                                  tgt.Phones <> src.Phones OR
                                  tgt.Mails <> src.Mails OR
                                  tgt.PCenterID <> src.PCenterID OR
                                  tgt.IsTOM <> src.IsTOM OR
                                  tgt.DeleteType <> src.DeleteType OR
                                  tgt.AuditoriumsCount <> src.AuditoriumsCount OR
                                  --tgt.CreateDate <> src.CreateDate OR
                                  --tgt.UpdateDate <> src.UpdateDate OR
                                  --tgt.ImportCreateDate <> src.ImportCreateDate OR
                                  --tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                                  tgt.ExamForm <> src.ExamForm OR
                                  tgt.VideoControl <> src.VideoControl OR
                                  tgt.AddressID <> src.AddressID OR
                                  tgt.StationFlags <> src.StationFlags OR
                                  tgt.TimeZoneId <> src.TimeZoneId)
                THEN UPDATE SET REGION = src.REGION,
                                AreaID = src.AreaID,
                                StationCode = src.StationCode,
                                StationName = src.StationName,
                                StationAddress = src.StationAddress,
                                SchoolFK = src.SchoolFK,
                                GovernmentID = src.GovernmentID,
                                sVolume = src.sVolume,
                                IsActive = src.IsActive,
                                Phones = src.Phones,
                                Mails = src.Mails,
                                PCenterID = src.PCenterID,
                                IsTOM = src.IsTOM,
                                DeleteType = src.DeleteType,
                                AuditoriumsCount = src.AuditoriumsCount,
                                --CreateDate = src.CreateDate,
                                UpdateDate = GETDATE(),
                                --ImportCreateDate = src.ImportCreateDate,
                                ImportUpdateDate = GETDATE(),
                                ExamForm = src.ExamForm,
                                VideoControl = src.VideoControl,
                                AddressID = src.AddressID,
                                StationFlags = src.StationFlags,
                                TimeZoneId = src.TimeZoneId
                WHEN NOT MATCHED BY SOURCE
                THEN UPDATE SET DeleteType = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

                SET @rows_count = @@ROWCOUNT;
                SET @end_date = GETDATE()
                SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
                RAISERROR (N'Загрузка ППЭ (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        END
        ELSE
            RAISERROR (N'Нет новых данных о ППЭ...', 0, 0) WITH NOWAIT;

        -- Загрузка аудиторий
        IF EXISTS(SELECT src.AuditoriumID
                    FROM loader.rbd_Auditoriums AS src
                   WHERE NOT EXISTS(SELECT tgt.AuditoriumID FROM dbo.rbd_Auditoriums AS tgt
                                     WHERE src.AuditoriumID = tgt.AuditoriumID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка аудиторий...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_Auditoriums AS tgt
            USING (SELECT a.*
                     FROM loader.rbd_Auditoriums AS a
                     WHERE EXISTS(SELECT st.StationID
                                    FROM dbo.rbd_Stations AS st
                                   WHERE a.StationID = st.StationID))
               AS src
               ON src.AuditoriumID = tgt.AuditoriumID
            WHEN NOT MATCHED
            THEN INSERT (REGION, AuditoriumID, StationID, AuditoriumCode, AuditoriumName, RowsCount,
                         ColsCount, OrganizerOrder, DeleteType, LimitPotencial, Imported, CreateDate,
                         UpdateDate, ImportCreateDate, ImportUpdateDate, ExamForm, VideoControl, IsLab)
            VALUES (src.REGION, src.AuditoriumID, src.StationID, src.AuditoriumCode, src.AuditoriumName,
                    src.RowsCount, src.ColsCount, src.OrganizerOrder, src.DeleteType, src.LimitPotencial,
                    src.Imported, src.CreateDate, src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate,
                    src.ExamForm, src.VideoControl, src.IsLab)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.StationID <> src.StationID OR
                              tgt.AuditoriumCode <> src.AuditoriumCode OR
                              tgt.AuditoriumName <> src.AuditoriumName OR
                              tgt.RowsCount <> src.RowsCount OR
                              tgt.ColsCount <> src.ColsCount OR
                              tgt.OrganizerOrder <> src.OrganizerOrder OR
                              tgt.DeleteType <> src.DeleteType OR
                              tgt.LimitPotencial <> src.LimitPotencial OR
                              tgt.Imported <> src.Imported OR
                              --tgt.CreateDate <> src.CreateDate OR
                              --tgt.UpdateDate <> src.UpdateDate OR
                              --tgt.ImportCreateDate <> src.ImportCreateDate OR
                              --tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.ExamForm <> src.ExamForm OR
                              tgt.VideoControl <> src.VideoControl OR
                              tgt.IsLab <> src.IsLab)
            THEN UPDATE SET REGION = src.REGION,
                            StationID = src.StationID,
                            AuditoriumCode = src.AuditoriumCode,
                            AuditoriumName = src.AuditoriumName,
                            RowsCount = src.RowsCount,
                            ColsCount = src.ColsCount,
                            OrganizerOrder = src.OrganizerOrder,
                            DeleteType = src.DeleteType,
                            LimitPotencial = src.LimitPotencial,
                            Imported = src.Imported,
                            --CreateDate = src.CreateDate,
                            UpdateDate = GETDATE(),
                            --ImportCreateDate = src.ImportCreateDate,
                            ImportUpdateDate = GETDATE(),
                            ExamForm = src.ExamForm,
                            VideoControl = src.VideoControl,
                            IsLab = src.IsLab
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET DeleteType = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка аудиторий (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных об аудиториях...', 0, 0) WITH NOWAIT;

        -- Загрузка мест в аудиториях
        IF EXISTS(SELECT src.PlacesID
                    FROM loader.rbd_Places AS src
                   WHERE NOT EXISTS(SELECT tgt.PlacesID FROM dbo.rbd_Places AS tgt
                                     WHERE src.PlacesID = tgt.PlacesID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка мест в аудиториях...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_Places AS tgt
            USING (SELECT p.PlacesID, p.REGION, p.AuditoriumID, p.[Row], p.Col, p.IsBad, p.IsDeleted, p.PlaceType
                     FROM loader.rbd_Places AS p
                     WHERE EXISTS(SELECT a.AuditoriumID
                                    FROM dbo.rbd_Auditoriums AS a
                                   WHERE a.AuditoriumID = p.AuditoriumID))
               AS src
               ON src.PlacesID = tgt.PlacesID
            WHEN NOT MATCHED
            THEN INSERT (PlacesID, REGION, AuditoriumID, [Row], Col, IsBad, IsDeleted, PlaceType)
            VALUES (src.PlacesID, src.REGION, src.AuditoriumID, src.[Row], src.Col, src.IsBad, src.IsDeleted, src.PlaceType)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.AuditoriumID <> src.AuditoriumID OR
                              tgt.[Row] <> src.[Row] OR
                              tgt.Col <> src.Col OR
                              tgt.IsBad <> src.IsBad OR
                              tgt.IsDeleted <> src.IsDeleted OR
                              tgt.PlaceType <> src.PlaceType)
            THEN UPDATE SET REGION = src.REGION,
                            AuditoriumID = src.AuditoriumID,
                            [Row] = src.[Row],
                            Col = src.Col,
                            IsBad = src.IsBad,
                            IsDeleted = src.IsDeleted,
                            PlaceType = src.PlaceType
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка мест в аудиториях (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о местах в аудиториях...', 0, 0) WITH NOWAIT;

        -- Загрузка информации о предметной специализации аудиторий
        IF EXISTS(SELECT src.ID
                    FROM loader.rbd_AuditoriumsSubjects AS src
                   WHERE NOT EXISTS(SELECT tgt.ID FROM dbo.rbd_AuditoriumsSubjects AS tgt
                                     WHERE src.ID = tgt.ID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка информации о предметной специализации аудиторий...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_AuditoriumsSubjects AS tgt
            USING (SELECT asj.ID, asj.SubjectCode, asj.AuditoriumID, asj.REGION, asj.IsDeleted
                     FROM loader.rbd_AuditoriumsSubjects AS asj
                     WHERE EXISTS(SELECT a.AuditoriumID
                                    FROM dbo.rbd_Auditoriums AS a
                                   WHERE a.AuditoriumID = asj.AuditoriumID) AND
                           EXISTS(SELECT s.SubjectCode
                                    FROM dbo.dat_Subjects AS s
                                   WHERE s.SubjectCode = asj.SubjectCode)
                  )
               AS src
               ON src.ID = tgt.ID
            WHEN NOT MATCHED
            THEN INSERT (ID, SubjectCode, AuditoriumID, REGION, IsDeleted)
            VALUES (src.ID, src.SubjectCode, src.AuditoriumID, src.REGION, src.IsDeleted)
            WHEN MATCHED AND (tgt.SubjectCode <> src.SubjectCode OR
                              tgt.AuditoriumID <> src.AuditoriumID OR
                              tgt.REGION <> src.REGION OR
                              tgt.IsDeleted <> src.IsDeleted)
            THEN UPDATE SET ID = src.ID,
                            SubjectCode = src.SubjectCode,
                            AuditoriumID = src.AuditoriumID,
                            REGION = src.REGION,
                            IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка информации о предметной специализации аудиторий (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о предметной специализации аудиторий...', 0, 0) WITH NOWAIT;

        -- Загрузка распределения экзаменов по ППЭ
        SELECT @nwd_count = COUNT(src.StationsExamsID)
          FROM loader.rbd_StationsExams AS src
         WHERE NOT EXISTS(SELECT tgt.StationsExamsID FROM dbo.rbd_StationsExams AS tgt
                                     WHERE src.StationsExamsID = tgt.StationsExamsID)
        IF @nwd_count > 0
        BEGIN
            SET @rows_count = 0;
            SET @crow = 0;
            SELECT @rows_count = COUNT(se.StationsExamsID)
              FROM loader.rbd_StationsExams AS se
             WHERE NOT EXISTS(SELECT s.StationID
                                FROM dbo.rbd_Stations AS s
                               WHERE s.StationID = se.StationID) OR
                   NOT EXISTS(SELECT de.ExamGlobalID
                                FROM dbo.dat_Exams AS de
                               WHERE de.ExamGlobalID = se.ExamGlobalID)

            IF @rows_count > 0
            BEGIN
                DECLARE secur INSENSITIVE CURSOR
                    FOR SELECT 'Для распределения ID: {' + CAST(se.StationsExamsID AS NVARCHAR(40)) + N'}:' +
                               CASE WHEN s.StationID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден ППЭ (' + CAST(ISNULL(se.StationID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END +
                               CASE WHEN de.ExamGlobalID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден экзамен (' + CAST(ISNULL(se.ExamGlobalID, N'NULL') AS NVARCHAR(10)) + N')' ELSE N'' END +
                               CASE WHEN dbl.StationID IS NOT NULL AND dbl.ExamGlobalID IS NOT NULL
                                    THEN NCHAR(13) + NCHAR(10) + N'    обнаружены дублирующие записи для ППЭ (' + CAST(dbl.StationID AS NVARCHAR(40)) + N') и экзамена (' +
                                    CAST(dbl.ExamGlobalID AS NVARCHAR(4)) + N'), общее количество: ' + CAST(dbl.Amount AS NVARCHAR(4))
                               ELSE N'' END
                         FROM loader.rbd_StationsExams AS se
                         OUTER APPLY (SELECT s.StationID
                                        FROM dbo.rbd_Stations AS s
                                       WHERE s.StationID = se.StationID) s
                         OUTER APPLY (SELECT de.ExamGlobalID
                                        FROM dbo.dat_Exams AS de
                                       WHERE de.ExamGlobalID = se.ExamGlobalID) AS de
                         OUTER APPLY(SELECT lse.StationID, lse.ExamGlobalID, COUNT(lse.StationsExamsID) AS Amount
                                       FROM loader.rbd_StationsExams AS lse WITH(NOLOCK)
                                      WHERE lse.StationID = se.StationID AND
                                            lse.ExamGlobalID = se.ExamGlobalID
                                      GROUP BY lse.StationID, lse.ExamGlobalID
                                     HAVING COUNT(lse.StationsExamsID) > 1) AS dbl
                         WHERE s.StationID IS NULL OR
                               de.ExamGlobalID IS NULL OR
                               dbl.StationID IS NOT NULL
                OPEN secur
                FETCH NEXT FROM secur
                 INTO @info
                WHILE @@FETCH_STATUS = 0 AND @crow < @errors_show
                BEGIN
                    RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                    FETCH NEXT FROM secur
                     INTO @info
                    SET @crow = @crow + 1
                END
                CLOSE secur
                DEALLOCATE secur
                SET @rows_count = @rows_count - @errors_show
                RAISERROR (N'+ %d ошибок синхронизации распределения экзаменов по ППЭ!', 8, 8, @rows_count ) WITH NOWAIT;
            END

            SET @crow = @nwd_count - @crow - @rows_count
            --RAISERROR ('Распределений без ошибок: %d (%d)...', 0, 0, @crow, @nwd_count) WITH NOWAIT;
            IF @crow > 0
            BEGIN
                SET @start_date = GETDATE()
                RAISERROR ('Загрузка распределения экзаменов по ППЭ...', 0, 0) WITH NOWAIT;
                MERGE dbo.rbd_StationsExams AS tgt
                USING (SELECT se.REGION, se.StationsExamsID, se.StationID, se.ExamGlobalID, se.PlacesCount,
                              se.LockOnStation, se.CreateDate, se.UpdateDate, se.ImportCreateDate,
                              se.ImportUpdateDate, se.IsDeleted, se.IsAutoAppoint
                         FROM loader.rbd_StationsExams AS se
                        WHERE EXISTS(SELECT s.StationID
                                       FROM dbo.rbd_Stations AS s
                                      WHERE s.StationID = se.StationID) AND
                              EXISTS(SELECT de.ExamGlobalID
                                       FROM dbo.dat_Exams AS de
                                      WHERE de.ExamGlobalID = se.ExamGlobalID) AND
                              NOT EXISTS(SELECT lse.StationID, lse.ExamGlobalID, COUNT(lse.StationsExamsID) AS Amount
                                           FROM loader.rbd_StationsExams AS lse WITH(NOLOCK)
                                          WHERE lse.StationID = se.StationID AND
                                                lse.ExamGlobalID = se.ExamGlobalID
                                          GROUP BY lse.StationID, lse.ExamGlobalID
                                         HAVING COUNT(lse.StationsExamsID) > 1)
                      )
                   AS src
                   ON src.StationsExamsID = tgt.StationsExamsID
                WHEN NOT MATCHED
                THEN INSERT (REGION, StationsExamsID, StationID, ExamGlobalID, PlacesCount, LockOnStation,
                             CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted, IsAutoAppoint)
                VALUES (src.REGION, src.StationsExamsID, src.StationID, src.ExamGlobalID, src.PlacesCount, src.LockOnStation,
                        src.CreateDate, src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate, src.IsDeleted, src.IsAutoAppoint)
                WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                                  tgt.StationID <> src.StationID OR
                                  tgt.ExamGlobalID <> src.ExamGlobalID OR
                                  tgt.PlacesCount <> src.PlacesCount OR
                                  tgt.LockOnStation <> src.LockOnStation OR
                                  tgt.CreateDate <> src.CreateDate OR
                                  tgt.UpdateDate <> src.UpdateDate OR
                                  tgt.ImportCreateDate <> src.ImportCreateDate OR
                                  tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                                  tgt.IsDeleted <> src.IsDeleted OR
                                  tgt.IsAutoAppoint <> src.IsAutoAppoint)
                THEN UPDATE SET REGION = src.REGION,
                                StationID = src.StationID,
                                ExamGlobalID = src.ExamGlobalID,
                                PlacesCount = src.PlacesCount,
                                LockOnStation = src.LockOnStation,
                                --CreateDate = src.CreateDate,
                                UpdateDate = GETDATE(),
                                --ImportCreateDate = src.ImportCreateDate,
                                ImportUpdateDate = GETDATE(),
                                IsDeleted = src.IsDeleted,
                                IsAutoAppoint = src.IsAutoAppoint
                WHEN NOT MATCHED BY SOURCE
                THEN UPDATE SET IsDeleted = 1;
                SET @rows_count = @@ROWCOUNT;
                SET @end_date = GETDATE()
                SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
                RAISERROR (N'Загрузка распределения экзаменов по ППЭ (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        END
        ELSE
            RAISERROR (N'Нет новых данных о распределении экзаменов по ППЭ...', 0, 0) WITH NOWAIT;

        -- Загрузка данных о распределении аудиторий на экзамены
        IF EXISTS(SELECT src.StationExamAuditoryID
                    FROM loader.rbd_StationExamAuditory AS src
                   WHERE NOT EXISTS(SELECT tgt.StationExamAuditoryID FROM dbo.rbd_StationExamAuditory AS tgt
                                     WHERE src.StationExamAuditoryID = tgt.StationExamAuditoryID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка данных о распределении аудиторий на экзамены...', 0, 0) WITH NOWAIT;

            MERGE dbo.rbd_StationExamAuditory AS tgt
            USING (SELECT sea.StationExamAuditoryID, sea.Region, sea.StationsExamsID, sea.StationID,
                          sea.AuditoriumID, sea.PlacesCount, sea.CreateDate, sea.UpdateDate, sea.ImportCreateDate,
                          sea.ImportUpdateDate, sea.IsDeleted, sea.IsPreparation, sea.ExamFormatCode, sea.IsAutoAppoint
                     FROM loader.rbd_StationExamAuditory AS sea
                     WHERE EXISTS(SELECT REGION
                                    FROM dbo.rbdc_Regions AS r
                                   WHERE r.REGION = sea.Region) AND
                           EXISTS(SELECT s.StationID
                                    FROM dbo.rbd_Stations AS s
                                   WHERE s.StationID = sea.StationID) AND
                           EXISTS(SELECT a.AuditoriumID
                                    FROM dbo.rbd_Auditoriums AS a
                                   WHERE a.AuditoriumID = sea.AuditoriumID) AND
                           EXISTS(SELECT se.StationsExamsID
                                    FROM dbo.rbd_StationsExams AS se
                                   WHERE se.StationsExamsID = sea.StationsExamsID)
                  )
               AS src
               ON src.StationExamAuditoryID = tgt.StationExamAuditoryID
            WHEN NOT MATCHED
            THEN INSERT (StationExamAuditoryID, Region, StationsExamsID, StationID, AuditoriumID, PlacesCount,
                         CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted, IsPreparation,
                         ExamFormatCode, IsAutoAppoint)
            VALUES (src.StationExamAuditoryID, src.Region, src.StationsExamsID, src.StationID, src.AuditoriumID,
                    src.PlacesCount, src.CreateDate, src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate,
                    src.IsDeleted, src.IsPreparation, src.ExamFormatCode, src.IsAutoAppoint)
            WHEN MATCHED AND (tgt.Region <> src.Region OR
                              tgt.StationsExamsID <> src.StationsExamsID OR
                              tgt.StationID <> src.StationID OR
                              tgt.AuditoriumID <> src.AuditoriumID OR
                              tgt.PlacesCount <> src.PlacesCount OR
                              tgt.CreateDate <> src.CreateDate OR
                              tgt.UpdateDate <> src.UpdateDate OR
                              tgt.ImportCreateDate <> src.ImportCreateDate OR
                              tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.IsDeleted <> src.IsDeleted OR
                              tgt.IsPreparation <> src.IsPreparation OR
                              tgt.ExamFormatCode <> src.ExamFormatCode OR
                              tgt.IsAutoAppoint <> src.IsAutoAppoint)
            THEN UPDATE SET Region = src.Region,
                            StationsExamsID = src.StationsExamsID,
                            StationID = src.StationID,
                            AuditoriumID = src.AuditoriumID,
                            PlacesCount = src.PlacesCount,
                            --CreateDate = src.CreateDate,
                            UpdateDate = GETDATE(),
                            --ImportCreateDate = src.ImportCreateDate,
                            ImportUpdateDate = GETDATE(),
                            IsDeleted = src.IsDeleted,
                            IsPreparation = src.IsPreparation,
                            ExamFormatCode = src.ExamFormatCode,
                            IsAutoAppoint = src.IsAutoAppoint
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка данных о распределении аудиторий на экзамены (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о распределении аудиторий на экзамены...', 0, 0) WITH NOWAIT;
        --===============================================================================--
        --===============================================================================--
        -- Загрузка участников
        --===============================================================================--
        IF EXISTS(SELECT src.ParticipantID
                    FROM loader.rbd_Participants AS src
                   WHERE NOT EXISTS(SELECT tgt.ParticipantID FROM dbo.rbd_Participants AS tgt
                                     WHERE src.ParticipantID = tgt.ParticipantID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка участников...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_Participants AS tgt
            USING (SELECT p.*
                     FROM loader.rbd_Participants AS p
                     WHERE EXISTS(SELECT REGION
                                   FROM dbo.rbdc_Regions AS r
                                  WHERE r.REGION = p.REGION) AND
                           EXISTS(SELECT DocumentTypeCode
                                    FROM dbo.rbdc_DocumentTypes AS dt
                                   WHERE dt.DocumentTypeCode = p.DocumentTypeCode) AND
                           EXISTS(SELECT CategoryID
                                    FROM dbo.rbdc_ParticipantCategories AS pk
                                   WHERE pk.CategoryID = p.ParticipantCategoryFK) AND
                           EXISTS(SELECT Code FROM dbo.rbdc_Study AS s WHERE s.Code = p.Study) AND
                           EXISTS(SELECT CitizenshipID FROM dbo.rbdc_Citizenship AS c WHERE c.CitizenshipID = p.CitizenshipID)
                     )
               AS src
               ON src.ParticipantID = tgt.ParticipantID
            WHEN NOT MATCHED
            THEN INSERT (ParticipantID, Region, ParticipantCode, Surname, Name, SecondName, DocumentSeries,
                         DocumentNumber, DocumentTypeCode, Sex, Gia, GiaAccept, pClass, BirthDay, Reserve1,
                         Reserve2, DeleteType, LimitPotencial, ParticipantDouble, FinishRegion,
                         ParticipantCategoryFK, SchoolRegistration, SchoolOutcoming, Study,
                         CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate, CitizenshipID,
                         SchoolOutcomingName)
            VALUES (src.ParticipantID, src.Region, src.ParticipantCode, src.Surname, src.Name, src.SecondName, src.DocumentSeries,
                    src.DocumentNumber, src.DocumentTypeCode, src.Sex, src.Gia, src.GiaAccept, src.pClass, src.BirthDay, src.Reserve1,
                    src.Reserve2, src.DeleteType, src.LimitPotencial, src.ParticipantDouble, src.FinishRegion,
                    src.ParticipantCategoryFK, src.SchoolRegistration, src.SchoolOutcoming, src.Study,
                    src.CreateDate, src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate, src.CitizenshipID,
                    src.SchoolOutcomingName)
            WHEN MATCHED AND (tgt.ParticipantCode <> src.ParticipantCode OR
                              tgt.Surname <> src.Surname OR
                              tgt.Name <> src.Name OR
                              tgt.SecondName <> src.SecondName OR
                              tgt.DocumentSeries <> src.DocumentSeries OR
                              tgt.DocumentNumber <> src.DocumentNumber OR
                              tgt.DocumentTypeCode <> src.DocumentTypeCode OR
                              tgt.Sex <> src.Sex OR
                              tgt.Gia <> src.Gia OR
                              tgt.GiaAccept <> src.GiaAccept OR
                              tgt.pClass <> src.pClass OR
                              tgt.BirthDay <> src.BirthDay OR
                              tgt.Reserve1 <> src.Reserve1 OR
                              tgt.Reserve2 <> src.Reserve2 OR
                              tgt.DeleteType <> src.DeleteType OR
                              tgt.LimitPotencial <> src.LimitPotencial OR
                              tgt.ParticipantDouble <> src.ParticipantDouble OR
                              tgt.FinishRegion <> src.FinishRegion OR
                              tgt.ParticipantCategoryFK <> src.ParticipantCategoryFK OR
                              tgt.SchoolRegistration <> src.SchoolRegistration OR
                              tgt.SchoolOutcoming <> src.SchoolOutcoming OR
                              tgt.Study <> src.Study OR
                              tgt.CreateDate <> src.CreateDate OR
                              tgt.UpdateDate <> src.UpdateDate OR
                              tgt.ImportCreateDate <> src.ImportCreateDate OR
                              tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.CitizenshipID <> src.CitizenshipID OR
                              tgt.SchoolOutcomingName <> src.SchoolOutcomingName)
                THEN UPDATE SET ParticipantCode = src.ParticipantCode,
                                Surname = src.Surname,
                                Name = src.Name,
                                SecondName = src.SecondName,
                                DocumentSeries = src.DocumentSeries,
                                DocumentNumber = src.DocumentNumber,
                                DocumentTypeCode = src.DocumentTypeCode,
                                Sex = src.Sex,
                                Gia = src.Gia,
                                GiaAccept = src.GiaAccept,
                                pClass = src.pClass,
                                BirthDay = src.BirthDay,
                                Reserve1 = src.Reserve1,
                                Reserve2 = src.Reserve2,
                                DeleteType = src.DeleteType,
                                LimitPotencial = src.LimitPotencial,
                                ParticipantDouble = src.ParticipantDouble,
                                FinishRegion = src.FinishRegion,
                                ParticipantCategoryFK = src.ParticipantCategoryFK,
                                SchoolRegistration = src.SchoolRegistration,
                                SchoolOutcoming = src.SchoolOutcoming,
                                Study = src.Study,
                                UpdateDate = GETDATE(),
                                ImportUpdateDate = GETDATE(),
                                CitizenshipID = src.CitizenshipID,
                                SchoolOutcomingName = src.SchoolOutcomingName
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET DeleteType = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка участников ГИА (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных об участниках ГИА...', 0, 0) WITH NOWAIT;

        -- Загрузка дополнительной информации об участниках
        IF EXISTS(SELECT src.PropertyId
                    FROM loader.rbd_ParticipantProperties AS src
                   WHERE NOT EXISTS(SELECT tgt.PropertyId FROM dbo.rbd_ParticipantProperties AS tgt
                                     WHERE src.PropertyId = tgt.PropertyId))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка дополнительной информации об участниках...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_ParticipantProperties AS tgt
            USING (SELECT pp.PropertyId, pp.ParticipantId, pp.Property, pp.PValue, pp.Region, pp.IsDeleted
                     FROM loader.rbd_ParticipantProperties AS pp
                     WHERE EXISTS(SELECT REGION
                                   FROM dbo.rbdc_Regions AS r
                                  WHERE r.REGION = pp.Region) AND
                           EXISTS(SELECT p.ParticipantID
                                    FROM dbo.rbd_Participants AS p
                                   WHERE p.ParticipantID = pp.ParticipantId)
                  )
               AS src
               ON src.PropertyId = tgt.PropertyId
            WHEN NOT MATCHED
            THEN INSERT (PropertyId, ParticipantId, Property, PValue, Region, IsDeleted)
            VALUES (src.PropertyId, src.ParticipantId, src.Property, src.PValue, src.Region, src.IsDeleted)
            WHEN MATCHED AND (tgt.ParticipantId <> src.ParticipantId OR
                              tgt.Property <> src.Property OR
                              tgt.PValue <> src.PValue OR
                              tgt.Region <> src.Region OR
                              tgt.IsDeleted <> src.IsDeleted)
                THEN UPDATE SET ParticipantId = src.ParticipantId,
                                Property = src.Property,
                                PValue = src.PValue,
                                Region = src.Region,
                                IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка дополнительной информации об участниках (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о дополнительной информации участников ГИА...', 0, 0) WITH NOWAIT;
        -- Загрузка списка заявлений участников ГИА по предметам
        IF EXISTS(SELECT src.ID
                    FROM loader.rbd_ParticipantsSubject AS src
                   WHERE NOT EXISTS(SELECT tgt.ID FROM dbo.rbd_ParticipantsSubject AS tgt
                                     WHERE src.ID = tgt.ID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка списка заявлений участников ГИА по предметам...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_ParticipantsSubject AS tgt
            USING (SELECT ps.REGION, ps.ID, ps.ParticipantID, ps.SubjectCode, ps.IsDeleted
                     FROM loader.rbd_ParticipantsSubject AS ps
                     WHERE EXISTS(SELECT REGION
                                   FROM dbo.rbdc_Regions AS r
                                  WHERE r.REGION = ps.REGION) AND
                           EXISTS(SELECT p.ParticipantID
                                    FROM dbo.rbd_Participants AS p
                                   WHERE p.ParticipantID = ps.ParticipantId) AND
                           EXISTS(SELECT s.SubjectGlobalID
                                    FROM dbo.dat_Subjects AS s
                                   WHERE s.SubjectCode = ps.SubjectCode)
                  )
               AS src
               ON src.ID = tgt.ID
            WHEN NOT MATCHED
            THEN INSERT (REGION, ID, ParticipantID, SubjectCode, IsDeleted)
            VALUES (src.REGION, src.ID, src.ParticipantID, src.SubjectCode, src.IsDeleted)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.ParticipantID <> src.ParticipantID OR
                              tgt.SubjectCode <> src.SubjectCode OR
                              tgt.IsDeleted <> src.IsDeleted)
                THEN UPDATE SET REGION = src.REGION,
                                ParticipantID = src.ParticipantID,
                                SubjectCode = src.SubjectCode,
                                IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка списка заявлений участников ГИА по предметам (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о заявлениях участников ГИА по предметам...', 0, 0) WITH NOWAIT;

        -- Загрузка списка специализированных предметов участников ГИА
        IF EXISTS(SELECT src.ID
                    FROM loader.rbd_ParticipantsProfSubject AS src
                   WHERE NOT EXISTS(SELECT tgt.ID FROM dbo.rbd_ParticipantsProfSubject AS tgt
                                     WHERE src.ID = tgt.ID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка списка специализированных предметов участников ГИА...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_ParticipantsProfSubject AS tgt
            USING (SELECT pps.REGION, pps.ID, pps.ParticipantID, pps.SubjectCode, pps.IsDeleted
                     FROM loader.rbd_ParticipantsProfSubject AS pps
                     WHERE EXISTS(SELECT REGION
                                   FROM dbo.rbdc_Regions AS r
                                  WHERE r.REGION = pps.REGION) AND
                           EXISTS(SELECT p.ParticipantID
                                    FROM dbo.rbd_Participants AS p
                                   WHERE p.ParticipantID = pps.ParticipantId) AND
                           EXISTS(SELECT s.SubjectGlobalID
                                    FROM dbo.dat_Subjects AS s
                                   WHERE s.SubjectCode = pps.SubjectCode)
                  )
               AS src
               ON src.ID = tgt.ID
            WHEN NOT MATCHED
            THEN INSERT (REGION, ID, ParticipantID, SubjectCode, IsDeleted)
            VALUES (src.REGION, src.ID, src.ParticipantID, src.SubjectCode, src.IsDeleted)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.ParticipantID <> src.ParticipantID OR
                              tgt.SubjectCode <> src.SubjectCode OR
                              tgt.IsDeleted <> src.IsDeleted)
                THEN UPDATE SET REGION = src.REGION,
                                ParticipantID = src.ParticipantID,
                                SubjectCode = src.SubjectCode,
                                IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка списка специализированных предметов участников ГИА (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о специализированных предметах участников ГИА...', 0, 0) WITH NOWAIT;

        -- Загрузка распределения экзаменов по участникам
        IF EXISTS(SELECT src.ParticipantsExamsID
                    FROM loader.rbd_ParticipantsExams AS src
                   WHERE NOT EXISTS(SELECT tgt.ParticipantsExamsID FROM dbo.rbd_ParticipantsExams AS tgt
                                     WHERE src.ParticipantsExamsID = tgt.ParticipantsExamsID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка распределения экзаменов по участникам...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_ParticipantsExams AS tgt
            USING (SELECT pe.REGION, pe.ParticipantID, pe.ExamGlobalID, pe.ParticipantsExamsID, pe.CreateDate,
                          pe.UpdateDate, pe.ImportCreateDate, pe.ImportUpdateDate, pe.IsDeleted, pe.ExamFormatCode
                     FROM loader.rbd_ParticipantsExams AS pe
                     WHERE EXISTS(SELECT REGION
                                    FROM dbo.rbdc_Regions AS r
                                   WHERE r.REGION = pe.Region) AND
                           EXISTS(SELECT p.ParticipantID
                                    FROM dbo.rbd_Participants AS p
                                   WHERE p.ParticipantID = pe.ParticipantId) AND
                           EXISTS(SELECT de.ExamGlobalID
                                    FROM dbo.dat_Exams AS de
                                   WHERE de.ExamGlobalID = pe.ExamGlobalID)
                  )
               AS src
               ON src.ParticipantsExamsID = tgt.ParticipantsExamsID
            WHEN NOT MATCHED
            THEN INSERT (REGION, ParticipantID, ExamGlobalID, ParticipantsExamsID, CreateDate,
                         UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted, ExamFormatCode)
            VALUES (src.REGION, src.ParticipantID, src.ExamGlobalID, src.ParticipantsExamsID, src.CreateDate,
                    src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate, src.IsDeleted, src.ExamFormatCode)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.ParticipantID <> src.ParticipantID OR
                              tgt.ExamGlobalID <> src.ExamGlobalID OR
                              tgt.CreateDate <> src.CreateDate OR
                              tgt.UpdateDate <> src.UpdateDate OR
                              tgt.ImportCreateDate <> src.ImportCreateDate OR
                              tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.IsDeleted <> src.IsDeleted OR
                              tgt.ExamFormatCode <> src.ExamFormatCode)
                THEN UPDATE SET REGION = src.REGION,
                                ParticipantID = src.ParticipantID,
                                ExamGlobalID = src.ExamGlobalID,
                                --CreateDate = src.CreateDate,
                                UpdateDate = GETDATE(),
                                --ImportCreateDate = src.ImportCreateDate,
                                ImportUpdateDate = GETDATE(),
                                IsDeleted = src.IsDeleted,
                                ExamFormatCode = src.ExamFormatCode
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка распределения экзаменов по участникам (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о распределении экзаменов по участникам ГИА...', 0, 0) WITH NOWAIT;
        
        -- Данные о распределении участников ГИА по ППЭ
        SELECT @nwd_count = COUNT(src.ParticipantsExamsOnStationID)
         FROM loader.rbd_ParticipantsExamsOnStation AS src
        WHERE NOT EXISTS(SELECT tgt.ParticipantsExamsOnStationID FROM dbo.rbd_ParticipantsExamsOnStation AS tgt
                          WHERE src.ParticipantsExamsOnStationID = tgt.ParticipantsExamsOnStationID) OR
              EXISTS(SELECT tgt.ParticipantsExamsOnStationID FROM dbo.rbd_ParticipantsExamsOnStation AS tgt
                      WHERE src.ParticipantsExamsOnStationID = tgt.ParticipantsExamsOnStationID AND
                            (tgt.ExamGlobalID <> src.ExamGlobalID OR
                             tgt.ParticipantsExamsID <> src.ParticipantsExamsID OR
                             tgt.StationsExamsID <> src.StationsExamsID OR
                             tgt.Region <> src.Region OR
                             tgt.IsDeleted <> src.IsDeleted OR
                             tgt.SessionID <> src.SessionID)
                    )
        IF @nwd_count > 0
        BEGIN
            SET @rows_count = 0;
            SET @crow = 0;
            SELECT @rows_count = COUNT(peos.ParticipantsExamsOnStationID)
              FROM loader.rbd_ParticipantsExamsOnStation AS peos
             WHERE NOT EXISTS(SELECT REGION
                                FROM dbo.rbdc_Regions AS r
                               WHERE r.REGION = peos.Region) OR
                   NOT EXISTS(SELECT pe.ParticipantsExamsID
                                FROM dbo.rbd_ParticipantsExams AS pe
                               WHERE pe.ParticipantsExamsID = peos.ParticipantsExamsID) OR
                   NOT EXISTS(SELECT de.ExamGlobalID
                                FROM dbo.dat_Exams AS de
                               WHERE de.ExamGlobalID = peos.ExamGlobalID) OR
                   NOT EXISTS(SELECT se.StationsExamsID
                                FROM dbo.rbd_StationsExams AS se
                               WHERE se.StationsExamsID = peos.StationsExamsID)

            IF @rows_count > 0
            BEGIN
                DECLARE secur INSENSITIVE CURSOR
                    FOR SELECT 'Для распределения участников ГИА по ППЭ ID: {' + CAST(peos.ParticipantsExamsOnStationID AS NVARCHAR(40)) + N'}:' +
                               CASE WHEN r.REGION IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден регион (' + CAST(ISNULL(peos.REGION, N'NULL') AS NVARCHAR(3)) + N')' ELSE N'' END +
                               CASE WHEN pe.ParticipantsExamsID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найдено распределение участников ГИА по экзаменам (' + CAST(ISNULL(peos.ParticipantsExamsID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END +
                               CASE WHEN de.ExamGlobalID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден экзамен (' + CAST(ISNULL(peos.ExamGlobalID, N'NULL') AS NVARCHAR(10)) + N')' ELSE N'' END +
                               CASE WHEN se.StationsExamsID IS NOT NULL THEN NCHAR(13) + NCHAR(10) + N'    не найдено распределение ППЭ по экзаменам (' + CAST(ISNULL(peos.StationsExamsID, N'NULL')  AS NVARCHAR(40)) + N')' ELSE N'' END
                         FROM loader.rbd_ParticipantsExamsOnStation AS peos
                         OUTER APPLY (SELECT REGION
                                        FROM dbo.rbdc_Regions AS r
                                       WHERE r.REGION = peos.Region) AS r
                         OUTER APPLY (SELECT pe.ParticipantsExamsID
                                        FROM dbo.rbd_ParticipantsExams AS pe
                                       WHERE pe.ParticipantsExamsID = peos.ParticipantsExamsID) AS pe
                         OUTER APPLY (SELECT de.ExamGlobalID
                                        FROM dbo.dat_Exams AS de
                                       WHERE de.ExamGlobalID = peos.ExamGlobalID) AS de
                         OUTER APPLY (SELECT se.StationsExamsID
                                        FROM dbo.rbd_StationsExams AS se
                                       WHERE se.StationsExamsID = peos.StationsExamsID) AS se
                         WHERE r.REGION IS NULL OR
                               pe.ParticipantsExamsID IS NULL OR
                               de.ExamGlobalID IS NULL OR
                               se.StationsExamsID IS NULL
                OPEN secur
                FETCH NEXT FROM secur
                 INTO @info
                WHILE @@FETCH_STATUS = 0 AND @crow < @errors_show
                BEGIN
                    RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                    FETCH NEXT FROM secur
                     INTO @info
                    SET @crow = @crow + 1
                END
                CLOSE secur
                DEALLOCATE secur
                SET @rows_count = @rows_count - @errors_show
                RAISERROR (N'+ %d ошибок синхронизации распределения экзаменов по ППЭ!', 8, 8, @rows_count ) WITH NOWAIT;
            END

            SET @crow = @nwd_count - @crow - @rows_count
            --RAISERROR ('Распределений без ошибок: %d (%d)...', 0, 0, @crow, @nwd_count) WITH NOWAIT;
            IF @crow > 0
            BEGIN
                SET @start_date = GETDATE()
                RAISERROR ('Загрузка распределения участников ГИА по ППЭ...', 0, 0) WITH NOWAIT;
                MERGE dbo.rbd_ParticipantsExamsOnStation AS tgt
                USING (SELECT peos.ParticipantsExamsOnStationID, peos.ExamGlobalID, peos.ParticipantsExamsID, peos.StationsExamsID, peos.Region,
                              peos.CreateDate, peos.UpdateDate, peos.ImportCreateDate, peos.ImportUpdateDate, peos.IsDeleted, peos.SessionID
                         FROM loader.rbd_ParticipantsExamsOnStation AS peos
                         WHERE EXISTS(SELECT REGION
                                        FROM dbo.rbdc_Regions AS r
                                       WHERE r.REGION = peos.Region) AND
                               EXISTS(SELECT pe.ParticipantsExamsID
                                        FROM dbo.rbd_ParticipantsExams AS pe
                                       WHERE pe.ParticipantsExamsID = peos.ParticipantsExamsID) AND
                               EXISTS(SELECT de.ExamGlobalID
                                        FROM dbo.dat_Exams AS de
                                       WHERE de.ExamGlobalID = peos.ExamGlobalID) AND
                               EXISTS(SELECT se.StationsExamsID
                                        FROM dbo.rbd_StationsExams AS se
                                       WHERE se.StationsExamsID = peos.StationsExamsID)
                      )
                   AS src
                   ON src.ParticipantsExamsID = tgt.ParticipantsExamsID
                WHEN NOT MATCHED
                THEN INSERT (ParticipantsExamsOnStationID, ExamGlobalID, ParticipantsExamsID, StationsExamsID, Region,
                             CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted, SessionID)
                VALUES (src.ParticipantsExamsOnStationID, src.ExamGlobalID, src.ParticipantsExamsID, src.StationsExamsID,
                        src.Region, src.CreateDate, src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate, src.IsDeleted,
                        src.SessionID)
                WHEN MATCHED AND (tgt.ExamGlobalID <> src.ExamGlobalID OR
                                  tgt.ParticipantsExamsID <> src.ParticipantsExamsID OR
                                  tgt.StationsExamsID <> src.StationsExamsID OR
                                  tgt.Region <> src.Region OR
                                  --tgt.CreateDate <> src.CreateDate OR
                                  --tgt.UpdateDate <> src.UpdateDate OR
                                  --tgt.ImportCreateDate <> src.ImportCreateDate OR
                                  --tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                                  tgt.IsDeleted <> src.IsDeleted OR
                                  tgt.SessionID <> src.SessionID)
                    THEN UPDATE SET ExamGlobalID = src.ExamGlobalID,
                                    ParticipantsExamsID = src.ParticipantsExamsID,
                                    StationsExamsID = src.StationsExamsID,
                                    Region = src.Region,
                                    --CreateDate = src.CreateDate,
                                    UpdateDate = GETDATE(),
                                    --ImportCreateDate = src.ImportCreateDate,
                                    ImportUpdateDate = GETDATE(),
                                    IsDeleted = src.IsDeleted,
                                    SessionID = src.SessionID
                WHEN NOT MATCHED BY SOURCE
                THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

                SET @rows_count = @@ROWCOUNT;
                SET @end_date = GETDATE()
                SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
                RAISERROR (N'Загрузка распределения участников ГИА по ППЭ (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        END
        ELSE
            RAISERROR (N'Нет новых данных о распределении участников ГИА по ППЭ...', 0, 0) WITH NOWAIT;

        -- Загрузка данных о рассадке участников ГИА
        IF EXISTS(SELECT src.PExamPlacesOnStationID
                    FROM loader.rbd_ParticipantsExamPStation AS src
                   WHERE NOT EXISTS(SELECT tgt.PExamPlacesOnStationID FROM dbo.rbd_ParticipantsExamPStation AS tgt
                                     WHERE src.PExamPlacesOnStationID = tgt.PExamPlacesOnStationID))
        BEGIN
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка данных о рассадке участников ГИА...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_ParticipantsExamPStation AS tgt
            USING (SELECT peps.PExamPlacesOnStationID, peps.PlacesID, peps.AuditoriumID,
                          peps.StationExamAuditoryID, peps.StationsExamsID, peps.ParticipantsExamsOnStationID,
                          peps.Region, peps.IsManual, peps.IsDeleted
                     FROM loader.rbd_ParticipantsExamPStation AS peps
                     WHERE EXISTS(SELECT REGION
                                    FROM dbo.rbdc_Regions AS r
                                   WHERE r.REGION = peps.Region) AND
                           EXISTS(SELECT p.PlacesID
                                    FROM dbo.rbd_Places AS p
                                   WHERE p.PlacesID = peps.PlacesID) AND
                           EXISTS(SELECT a.AuditoriumID
                                    FROM dbo.rbd_Auditoriums AS a
                                   WHERE a.AuditoriumID = peps.AuditoriumID) AND
                           EXISTS(SELECT sea.StationExamAuditoryID
                                    FROM dbo.rbd_StationExamAuditory AS sea
                                   WHERE sea.StationExamAuditoryID = peps.StationExamAuditoryID) AND
                           EXISTS(SELECT se.StationsExamsID
                                    FROM dbo.rbd_StationsExams AS se
                                   WHERE se.StationsExamsID = peps.StationsExamsID) AND
                           EXISTS(SELECT peos.ParticipantsExamsOnStationID
                                    FROM dbo.rbd_ParticipantsExamsOnStation AS peos
                                   WHERE peos.ParticipantsExamsOnStationID = peps.ParticipantsExamsOnStationID)
                  )
               AS src
               ON src.PExamPlacesOnStationID = tgt.PExamPlacesOnStationID
            WHEN NOT MATCHED
            THEN INSERT (PExamPlacesOnStationID, PlacesID, AuditoriumID, StationExamAuditoryID,
                         StationsExamsID, ParticipantsExamsOnStationID, Region,
                         IsManual, IsDeleted)
            VALUES (src.PExamPlacesOnStationID, src.PlacesID, src.AuditoriumID, src.StationExamAuditoryID,
                    src.StationsExamsID, src.ParticipantsExamsOnStationID, src.Region,
                    src.IsManual, src.IsDeleted)
            WHEN MATCHED AND (tgt.PlacesID <> src.PlacesID OR
                              tgt.AuditoriumID <> src.AuditoriumID OR
                              tgt.StationExamAuditoryID <> src.StationExamAuditoryID OR
                              tgt.StationsExamsID <> src.StationsExamsID OR
                              tgt.ParticipantsExamsOnStationID <> src.ParticipantsExamsOnStationID OR
                              tgt.Region <> src.Region OR
                              tgt.IsManual <> src.IsManual OR
                              tgt.IsDeleted <> src.IsDeleted)
                THEN UPDATE SET PlacesID = src.PlacesID,
                                AuditoriumID = src.AuditoriumID,
                                StationExamAuditoryID = src.StationExamAuditoryID,
                                StationsExamsID = src.StationsExamsID,
                                ParticipantsExamsOnStationID = src.ParticipantsExamsOnStationID,
                                Region = src.Region,
                                IsManual = src.IsManual,
                                IsDeleted = src.IsDeleted
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка данных о рассадке участников ГИА (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных о данных о рассадке участников ГИА...', 0, 0) WITH NOWAIT;
        --===============================================================================--

        --===============================================================================--
        -- Загрузка экспертов
        --===============================================================================--

        SELECT @nwd_count = COUNT(src.ExpertID)
          FROM loader.rbd_Experts AS src
         WHERE NOT EXISTS(SELECT tgt.ExpertID FROM dbo.rbd_Experts AS tgt
                           WHERE src.ExpertID = tgt.ExpertID)

        IF @nwd_count > 0
        BEGIN
            SET @crow = 0
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка экспертов...', 0, 0) WITH NOWAIT;
            -- Проверка экспертов
            DECLARE ccur INSENSITIVE CURSOR
                FOR SELECT N'Для эксперта РЦОИ ID: {' + CAST(ex.ExpertID AS NVARCHAR(40)) + N'}:' +
                           CASE WHEN dt.DocumentTypeCode IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверный тип документа;' ELSE N'' END +
                           CASE WHEN et.EduTypeID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверный уровень образования;' ELSE N'' END +
                           CASE WHEN ek.EduKindID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверное ученое звание;'  ELSE N'' END +
                           CASE WHEN ec.ExpertCategoryID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверная категория эксперта;'  ELSE N'' END +
                           CASE WHEN g.GovernmentID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверное МОУО для эксперта;'  ELSE N'' END +
                           CASE WHEN s.SchoolID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверная ОО для эксперта;'  ELSE N'' END
                      FROM loader.rbd_Experts AS ex
                     OUTER APPLY (SELECT dt.DocumentTypeCode
                                    FROM dbo.rbdc_DocumentTypes AS dt
                                   WHERE dt.DocumentTypeCode = ex.DocumentTypeCode) AS dt
                     OUTER APPLY (SELECT et.EduTypeID
                                    FROM dbo.rbdc_EducationTypes AS et
                                   WHERE et.EduTypeID = ex.EduTypeFK) AS et
                     OUTER APPLY (SELECT ek.EduKindID
                                    FROM dbo.rbdc_EducationKinds AS ek
                                   WHERE ek.EduKindID = ex.EduKindFK) AS ek
                     OUTER APPLY (SELECT ec.ExpertCategoryID
                                    FROM dbo.rbdc_ExpertCategories AS ec
                                   WHERE ec.ExpertCategoryID = ex.ExpertCategoryID) AS ec
                     OUTER APPLY (SELECT g.GovernmentID
                                    FROM dbo.rbd_Governments AS g
                                   WHERE g.GovernmentID = ex.GovernmentID) AS g
                     OUTER APPLY (SELECT s.SchoolID
                                    FROM dbo.rbd_Schools AS s
                                   WHERE s.SchoolID = ex.SchoolID) AS s
                     WHERE dt.DocumentTypeCode IS NULL OR
                           --et.EduTypeID IS NULL OR
                           --ek.EduKindID IS NULL OR
                           ec.ExpertCategoryID IS NULL --OR
                           --g.GovernmentID IS NULL --OR
                           --s.SchoolID IS NULL
            OPEN ccur
            FETCH NEXT FROM ccur
             INTO @info
            WHILE @@FETCH_STATUS = 0 AND @crow < @errors_show
            BEGIN
        
                RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                FETCH NEXT FROM ccur
                 INTO @info
                SET @crow = @crow + 1
            END
            CLOSE ccur
            DEALLOCATE ccur

            MERGE dbo.rbd_Experts AS tgt
            USING (SELECT ex.*
                 FROM loader.rbd_Experts AS ex
                 WHERE EXISTS(SELECT DocumentTypeCode
                                FROM dbo.rbdc_DocumentTypes AS dt
                               WHERE dt.DocumentTypeCode = ex.DocumentTypeCode) AND
                       --EXISTS(SELECT et.EduTypeID
                       --         FROM dbo.rbdc_EducationTypes AS et
                       --        WHERE et.EduTypeID = ex.EduTypeFK) AND
                       --EXISTS(SELECT ek.EduKindID
                       --         FROM dbo.rbdc_EducationKinds AS ek
                       --        WHERE ek.EduKindID = ex.EduKindFK) AND
                       EXISTS(SELECT ec.ExpertCategoryID
                                FROM dbo.rbdc_ExpertCategories AS ec
                               WHERE ec.ExpertCategoryID = ex.ExpertCategoryID) --AND
                       --EXISTS(SELECT g.GovernmentID
                       --         FROM dbo.rbd_Governments AS g
                       --        WHERE g.GovernmentID = ex.GovernmentID) AND
                       --EXISTS(SELECT s.SchoolID
                       --         FROM dbo.rbd_Schools AS s
                       --        WHERE s.SchoolID = ex.SchoolID)
                 )
               AS src
               ON src.ExpertID = tgt.ExpertID
             WHEN NOT MATCHED
             THEN INSERT (ExpertID, REGION, ExpertCode, Surname, Name, SecondName, DocumentSeries, DocumentNumber,
                          DocumentTypeCode, Sex, EduTypeFK, EduKindFK, Seniority, PrecedingYear, BirthYear, IsDeleted,
                          Positions, SchoolID, NotSchoolJob, ThirdVerifyAcc, CreateDate, UpdateDate, ImportCreateDate,
                          ImportUpdateDate, GovernmentID, InConflictCommission, Qualification, ExpertCategoryID)
                  VALUES (src.ExpertID, src.REGION, src.ExpertCode, src.Surname, src.Name, src.SecondName, src.DocumentSeries,
                          src.DocumentNumber, src.DocumentTypeCode, src.Sex, src.EduTypeFK, src.EduKindFK, src.Seniority,
                          src.PrecedingYear, src.BirthYear, src.IsDeleted, src.Positions, src.SchoolID, src.NotSchoolJob,
                          src.ThirdVerifyAcc, src.CreateDate, src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate,
                          src.GovernmentID, src.InConflictCommission, src.Qualification, src.ExpertCategoryID)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.ExpertCode <> src.ExpertCode OR
                              tgt.Surname <> src.Surname OR
                              tgt.Name <> src.Name OR
                              tgt.SecondName <> src.SecondName OR
                              tgt.DocumentSeries <> src.DocumentSeries OR
                              tgt.DocumentNumber <> src.DocumentNumber OR
                              tgt.DocumentTypeCode <> src.DocumentTypeCode OR
                              tgt.Sex <> src.Sex OR
                              tgt.EduTypeFK <> src.EduTypeFK OR
                              tgt.EduKindFK <> src.EduKindFK OR
                              tgt.Seniority <> src.Seniority OR
                              tgt.PrecedingYear <> src.PrecedingYear OR
                              tgt.BirthYear <> src.BirthYear OR
                              tgt.IsDeleted <> src.IsDeleted OR
                              tgt.Positions <> src.Positions OR
                              tgt.SchoolID <> src.SchoolID OR
                              tgt.NotSchoolJob <> src.NotSchoolJob OR
                              tgt.ThirdVerifyAcc <> src.ThirdVerifyAcc OR
                              tgt.CreateDate <> src.CreateDate OR
                              tgt.UpdateDate <> src.UpdateDate OR
                              tgt.ImportCreateDate <> src.ImportCreateDate OR
                              tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.GovernmentID <> src.GovernmentID OR
                              tgt.InConflictCommission <> src.InConflictCommission OR
                              tgt.Qualification <> src.Qualification OR
                              tgt.ExpertCategoryID <> src.ExpertCategoryID)
                THEN UPDATE SET REGION = src.REGION,
                                ExpertCode = src.ExpertCode,
                                Surname = src.Surname,
                                Name = src.Name,
                                SecondName = src.SecondName,
                                DocumentSeries = src.DocumentSeries,
                                DocumentNumber = src.DocumentNumber,
                                DocumentTypeCode = src.DocumentTypeCode,
                                Sex = src.Sex,
                                EduTypeFK = src.EduTypeFK,
                                EduKindFK = src.EduKindFK,
                                Seniority = src.Seniority,
                                PrecedingYear = src.PrecedingYear,
                                BirthYear = src.BirthYear,
                                IsDeleted = src.IsDeleted,
                                Positions = src.Positions,
                                SchoolID = src.SchoolID,
                                NotSchoolJob = src.NotSchoolJob,
                                ThirdVerifyAcc = src.ThirdVerifyAcc,
                                UpdateDate = GETDATE(),
                                ImportUpdateDate = GETDATE(),
                                GovernmentID = src.GovernmentID,
                                InConflictCommission = src.InConflictCommission,
                                Qualification = src.Qualification,
                                ExpertCategoryID = src.ExpertCategoryID
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка экспертов (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных об экспертах...', 0, 0) WITH NOWAIT;

        -- Загрузка распределения на экзамены для экспертов
        SELECT @nwd_count = COUNT(src.ID)
          FROM loader.rbd_ExpertsExams AS src
         WHERE NOT EXISTS(SELECT tgt.ID FROM dbo.rbd_ExpertsExams AS tgt
                           WHERE src.ID = tgt.ID)

        IF @nwd_count > 0
        BEGIN
            SET @crow = 0
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка распределения на экзамены для экспертов...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_ExpertsExams AS tgt
            USING (SELECT ee.REGION, ee.ID, ee.ExpertID, ee.ExamGlobalID, ee.CreateDate, ee.UpdateDate,
                          ee.ImportCreateDate, ee.ImportUpdateDate, ee.IsDeleted, ee.StationsExamsID,
                          ee.CheckFormOnExam
                 FROM loader.rbd_ExpertsExams AS ee
                 WHERE EXISTS(SELECT ex.ExpertID
                                FROM dbo.rbd_Experts AS ex
                               WHERE ex.ExpertID = ee.ExpertID) AND
                       EXISTS(SELECT se.StationsExamsID
                                FROM dbo.rbd_StationsExams AS se
                               WHERE se.StationsExamsID = ee.StationsExamsID) AND
                       EXISTS(SELECT de.ExamGlobalID
                                FROM dbo.dat_Exams AS de
                               WHERE de.ExamGlobalID = ee.ExamGlobalID)
                 )
               AS src
               ON src.ID = tgt.ID
             WHEN NOT MATCHED
             THEN INSERT (REGION, ID, ExpertID, ExamGlobalID, CreateDate, UpdateDate, ImportCreateDate,
                          ImportUpdateDate, IsDeleted, StationsExamsID, CheckFormOnExam)
                  VALUES (src.REGION, src.ID, src.ExpertID, src.ExamGlobalID, src.CreateDate, src.UpdateDate, src.ImportCreateDate,
                          src.ImportUpdateDate, src.IsDeleted, src.StationsExamsID, src.CheckFormOnExam)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.ExpertID <> src.ExpertID OR
                              tgt.ExamGlobalID <> src.ExamGlobalID OR
                              tgt.CreateDate <> src.CreateDate OR
                              tgt.UpdateDate <> src.UpdateDate OR
                              tgt.ImportCreateDate <> src.ImportCreateDate OR
                              tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.IsDeleted <> src.IsDeleted OR
                              tgt.StationsExamsID <> src.StationsExamsID OR
                              tgt.CheckFormOnExam <> src.CheckFormOnExam)
                THEN UPDATE SET REGION = src.REGION,
                                ExpertID = src.ExpertID,
                                ExamGlobalID = src.ExamGlobalID,
                                --CreateDate = src.CreateDate,
                                UpdateDate = GETDATE(),
                                --ImportCreateDate = src.ImportCreateDate,
                                ImportUpdateDate = GETDATE(),
                                IsDeleted = src.IsDeleted,
                                StationsExamsID = src.StationsExamsID,
                                CheckFormOnExam = src.CheckFormOnExam
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка распределения на экзамены для экспертов (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        ELSE
            RAISERROR (N'Нет новых данных распределения на экзамены для экспертов...', 0, 0) WITH NOWAIT;

        -- загрузка предметной специализации экспертов
        SELECT @nwd_count = COUNT(src.ID)
          FROM loader.rbd_ExpertsSubjects AS src
         WHERE NOT EXISTS(SELECT tgt.ID FROM dbo.rbd_ExpertsSubjects AS tgt
                           WHERE src.ID = tgt.ID) OR
               EXISTS(SELECT tgt.ID
                        FROM dbo.rbd_ExpertsSubjects AS tgt
                       WHERE src.ID = tgt.ID AND (tgt.REGION <> src.REGION OR
                             tgt.ExpertID <> src.ExpertID OR
                             tgt.SubjectCode <> src.SubjectCode OR
                             tgt.IsDeleted <> src.IsDeleted OR
                             tgt.CheckForm <> src.CheckForm OR
                             tgt.ExpertCategoryID <> src.ExpertCategoryID OR
                             tgt.ThirdVerifyAcc <> src.ThirdVerifyAcc))
        IF @nwd_count > 0
        BEGIN
            SET @crow = 0
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка предметной специализации экспертов...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_ExpertsSubjects AS tgt
            USING (SELECT es.REGION, es.ID, es.ExpertID, es.SubjectCode, es.IsDeleted,
                          es.CheckForm, es.ExpertCategoryID, es.ThirdVerifyAcc
                 FROM loader.rbd_ExpertsSubjects AS es
                 WHERE EXISTS(SELECT REGION
                                    FROM dbo.rbdc_Regions AS r
                                   WHERE r.REGION = es.REGION) AND
                       EXISTS(SELECT ex.ExpertID
                                FROM dbo.rbd_Experts AS ex
                               WHERE ex.ExpertID = es.ExpertID) AND
                       EXISTS(SELECT ds.SubjectCode
                                FROM dbo.dat_Subjects AS ds
                               WHERE ds.SubjectCode = es.SubjectCode) AND
                       es.CheckForm IN (0, 1)
                 )
               AS src
               ON src.ID = tgt.ID
             WHEN NOT MATCHED
             THEN INSERT (REGION, ID, ExpertID, SubjectCode, IsDeleted,
                          CheckForm, ExpertCategoryID, ThirdVerifyAcc)
                  VALUES (src.REGION, src.ID, src.ExpertID, src.SubjectCode, src.IsDeleted,
                          src.CheckForm, src.ExpertCategoryID, src.ThirdVerifyAcc)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.ExpertID <> src.ExpertID OR
                              tgt.SubjectCode <> src.SubjectCode OR
                              tgt.IsDeleted <> src.IsDeleted OR
                              tgt.CheckForm <> src.CheckForm OR
                              tgt.ExpertCategoryID <> src.ExpertCategoryID OR
                              tgt.ThirdVerifyAcc <> src.ThirdVerifyAcc)
                THEN UPDATE SET REGION = src.REGION,
                                ExpertID = src.ExpertID,
                                SubjectCode = src.SubjectCode,
                                IsDeleted = src.IsDeleted,
                                CheckForm = src.CheckForm,
                                ExpertCategoryID = src.ExpertCategoryID,
                                ThirdVerifyAcc = src.ThirdVerifyAcc
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET IsDeleted = 1;

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка предметной специализации экспертов (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        ELSE
            RAISERROR (N'Нет новых данных о предметной специализации экспертов...', 0, 0) WITH NOWAIT;
        --===============================================================================--

        --===============================================================================--
        -- Загрузка работников ППЭ
        --===============================================================================--
        -- Загрузка данных об организаторах ГИА
        SELECT @nwd_count = COUNT(src.StationWorkerID)
          FROM loader.rbd_StationWorkers AS src
         WHERE NOT EXISTS(SELECT tgt.StationWorkerID FROM dbo.rbd_StationWorkers AS tgt
                           WHERE src.StationWorkerID = tgt.StationWorkerID) OR
               EXISTS(SELECT tgt.StationWorkerID
                        FROM dbo.rbd_StationWorkers AS tgt
                       WHERE src.StationWorkerID = tgt.StationWorkerID AND
                            (tgt.DocumentTypeCode <> src.DocumentTypeCode OR
                             tgt.StationWorkerCode <> src.StationWorkerCode OR
                             tgt.Surname <> src.Surname OR
                             tgt.Name <> src.Name OR
                             tgt.SecondName <> src.SecondName OR
                             tgt.DocumentSeries <> src.DocumentSeries OR
                             tgt.DocumentNumber <> src.DocumentNumber OR
                             tgt.Sex <> src.Sex OR
                             tgt.BirthYear <> src.BirthYear OR
                             tgt.SchoolPosition <> src.SchoolPosition OR
                             tgt.NotSchoolJob <> src.NotSchoolJob OR
                             tgt.DeleteType <> src.DeleteType OR
                             tgt.GovernmentID <> src.GovernmentID OR
                             tgt.SchoolID <> src.SchoolID OR
                             tgt.WorkerPositionID <> src.WorkerPositionID OR
                             tgt.Imported <> src.Imported OR
                             tgt.PrecedingYear <> src.PrecedingYear OR
                             tgt.Seniority <> src.Seniority OR
                             tgt.EducationTypeID <> src.EducationTypeID OR
                             tgt.SWorkerCategory <> src.SWorkerCategory OR
                             tgt.CertificateKeyID <> src.CertificateKeyID OR
                             tgt.Phones <> src.Phones OR
                             tgt.Mails <> src.Mails))
        IF @nwd_count > 0
        BEGIN
            SET @crow = 0
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка данных об организаторах ГИА...', 0, 0) WITH NOWAIT;
            MERGE dbo.rbd_StationWorkers AS tgt
            USING (SELECT sw.StationWorkerID, sw.REGION, sw.DocumentTypeCode, sw.StationWorkerCode,
                          sw.Surname, sw.Name, sw.SecondName, sw.DocumentSeries, sw.DocumentNumber,
                          sw.Sex, sw.BirthYear, sw.SchoolPosition, sw.NotSchoolJob, sw.DeleteType,
                          sw.GovernmentID, sw.SchoolID, sw.WorkerPositionID, sw.Imported,
                          sw.CreateDate, sw.UpdateDate, sw.ImportCreateDate, sw.ImportUpdateDate,
                          sw.PrecedingYear, sw.Seniority, sw.EducationTypeID, sw.SWorkerCategory,
                          sw.CertificateKeyID, sw.Phones, sw.Mails
                 FROM loader.rbd_StationWorkers AS sw
                 WHERE EXISTS(SELECT REGION
                                    FROM dbo.rbdc_Regions AS r
                                   WHERE r.REGION = sw.REGION) --AND
                       --EXISTS(SELECT DocumentTypeCode
                       --         FROM dbo.rbdc_DocumentTypes AS dt
                       --        WHERE dt.DocumentTypeCode = sw.DocumentTypeCode)
                  )
               AS src
               ON src.StationWorkerID = tgt.StationWorkerID AND
                  src.REGION = tgt.REGION
             WHEN NOT MATCHED
             THEN INSERT (StationWorkerID, REGION, DocumentTypeCode, StationWorkerCode, Surname,
                          Name, SecondName, DocumentSeries, DocumentNumber, Sex, BirthYear,
                          SchoolPosition, NotSchoolJob, DeleteType, GovernmentID, SchoolID,
                          WorkerPositionID, Imported, CreateDate, UpdateDate, ImportCreateDate,
                          ImportUpdateDate, PrecedingYear, Seniority, EducationTypeID,
                          SWorkerCategory, CertificateKeyID, Phones, Mails)
                  VALUES (src.StationWorkerID, src.REGION, src.DocumentTypeCode, src.StationWorkerCode, src.Surname,
                          src.Name, src.SecondName, src.DocumentSeries, src.DocumentNumber, src.Sex, src.BirthYear,
                          src.SchoolPosition, src.NotSchoolJob, src.DeleteType, src.GovernmentID, src.SchoolID,
                          src.WorkerPositionID, src.Imported, src.CreateDate, src.UpdateDate, src.ImportCreateDate,
                          src.ImportUpdateDate, src.PrecedingYear, src.Seniority, src.EducationTypeID,
                          src.SWorkerCategory, src.CertificateKeyID, src.Phones, src.Mails)
            WHEN MATCHED AND (tgt.DocumentTypeCode <> src.DocumentTypeCode OR
                              tgt.StationWorkerCode <> src.StationWorkerCode OR
                              tgt.Surname <> src.Surname OR
                              tgt.Name <> src.Name OR
                              tgt.SecondName <> src.SecondName OR
                              tgt.DocumentSeries <> src.DocumentSeries OR
                              tgt.DocumentNumber <> src.DocumentNumber OR
                              tgt.Sex <> src.Sex OR
                              tgt.BirthYear <> src.BirthYear OR
                              tgt.SchoolPosition <> src.SchoolPosition OR
                              tgt.NotSchoolJob <> src.NotSchoolJob OR
                              tgt.DeleteType <> src.DeleteType OR
                              tgt.GovernmentID <> src.GovernmentID OR
                              tgt.SchoolID <> src.SchoolID OR
                              tgt.WorkerPositionID <> src.WorkerPositionID OR
                              tgt.Imported <> src.Imported OR
                              --tgt.CreateDate <> src.CreateDate OR
                              --tgt.UpdateDate <> src.UpdateDate OR
                              --tgt.ImportCreateDate <> src.ImportCreateDate OR
                              --tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                              tgt.PrecedingYear <> src.PrecedingYear OR
                              tgt.Seniority <> src.Seniority OR
                              tgt.EducationTypeID <> src.EducationTypeID OR
                              tgt.SWorkerCategory <> src.SWorkerCategory OR
                              tgt.CertificateKeyID <> src.CertificateKeyID OR
                              tgt.Phones <> src.Phones OR
                              tgt.Mails <> src.Mails)
                THEN UPDATE SET DocumentTypeCode = src.DocumentTypeCode,
                                StationWorkerCode = src.StationWorkerCode,
                                Surname = src.Surname,
                                Name = src.Name,
                                SecondName = src.SecondName,
                                DocumentSeries = src.DocumentSeries,
                                DocumentNumber = src.DocumentNumber,
                                Sex = src.Sex,
                                BirthYear = src.BirthYear,
                                SchoolPosition = src.SchoolPosition,
                                NotSchoolJob = src.NotSchoolJob,
                                DeleteType = src.DeleteType,
                                GovernmentID = src.GovernmentID,
                                SchoolID = src.SchoolID,
                                WorkerPositionID = src.WorkerPositionID,
                                Imported = src.Imported,
                                --CreateDate = src.CreateDate,
                                UpdateDate = GETDATE(),
                                --ImportCreateDate = src.ImportCreateDate,
                                ImportUpdateDate = GETDATE(),
                                PrecedingYear = src.PrecedingYear,
                                Seniority = src.Seniority,
                                EducationTypeID = src.EducationTypeID,
                                SWorkerCategory = src.SWorkerCategory,
                                CertificateKeyID = src.CertificateKeyID,
                                Phones = src.Phones,
                                Mails = src.Mails
            WHEN NOT MATCHED BY SOURCE
            THEN UPDATE SET DeleteType = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();
            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка данных об организаторах ГИА (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END
        ELSE
            RAISERROR (N'Нет новых данных об организаторах ГИА...', 0, 0) WITH NOWAIT;


        DECLARE csws INSENSITIVE CURSOR
            FOR SELECT N'Для записи ID: {' + CAST(sws.ID AS NVARCHAR(40)) + N'}' +
                       N' предмет ' + CASE WHEN ISNULL(sws.SubjectCode, 0) < 10 THEN N'0' ELSE N'' END +
                       CAST(ISNULL(sws.SubjectCode, 0) AS NVARCHAR(2)) + N':' + 
                       CASE WHEN swsr.REGION IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверно указан код субъекта РФ' ELSE N'' END +
                       CASE WHEN swsw.StationWorkerID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверно указан идентификатор работника ППЭ' ELSE N'' END +
                       CASE WHEN swss.SubjectCode IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверно указан код предмета' ELSE N'' END
                 FROM loader.rbd_StationWorkersSubjects AS sws
                OUTER APPLY(SELECT r.REGION
                              FROM dbo.rbdc_Regions AS r
                             WHERE r.REGION = sws.REGION) AS swsr
                OUTER APPLY(SELECT sw.StationWorkerID
                              FROM rbd_StationWorkers AS sw
                             WHERE sw.StationWorkerID = sws.StationWorkerID) AS swsw
                OUTER APPLY(SELECT s.SubjectCode
                              FROM dat_Subjects AS s
                             WHERE s.SubjectCode = sws.SubjectCode) AS swss
                WHERE swsr.REGION IS NULL OR
                      swsw.StationWorkerID IS NULL OR
                      swss.SubjectCode IS NULL
        OPEN csws
        FETCH NEXT FROM csws
         INTO @info
        WHILE @@FETCH_STATUS = 0
        BEGIN
        
            RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
            FETCH NEXT FROM csws
             INTO @info
        END
        CLOSE csws
        DEALLOCATE csws

        -- Загрузка предметной специализации организаторов ГИА
        SELECT @nwd_count = COUNT(src.ID)
         FROM loader.rbd_StationWorkersSubjects AS src
        WHERE NOT EXISTS(SELECT tgt.ID FROM dbo.rbd_StationWorkersSubjects AS tgt
                          WHERE src.ID = tgt.ID) OR
              EXISTS(SELECT tgt.ID FROM dbo.rbd_StationWorkersSubjects AS tgt
                      WHERE src.ID = tgt.ID AND
                            (tgt.REGION <> src.REGION OR
                             tgt.SubjectCode <> src.SubjectCode OR
                             tgt.StationWorkerID <> src.StationWorkerID OR
                             tgt.IsDeleted <> src.IsDeleted)
                    )
        IF @nwd_count > 0
        BEGIN
            SET @rows_count = 0;
            SET @crow = 0;
            SELECT @rows_count = COUNT(sws.ID)
              FROM loader.rbd_StationWorkersSubjects AS sws
             WHERE NOT EXISTS(SELECT r.REGION
                                FROM dbo.rbdc_Regions AS r
                               WHERE r.REGION = sws.REGION) OR
                   NOT EXISTS(SELECT sw.StationWorkerID
                                FROM rbd_StationWorkers AS sw
                               WHERE sw.StationWorkerID = sws.StationWorkerID) OR
                   NOT EXISTS(SELECT s.SubjectCode
                                FROM dat_Subjects AS s
                               WHERE s.SubjectCode = sws.SubjectCode)

            IF @rows_count > 0
            BEGIN
                DECLARE swcur INSENSITIVE CURSOR
                    FOR SELECT 'Для предметной специализации организаторов ГИА ID: {' + CAST(sws.ID AS NVARCHAR(40)) + N'}:' +
                               CASE WHEN r.REGION IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден регион (' + CAST(ISNULL(sws.REGION, N'NULL') AS NVARCHAR(3)) + N')' ELSE N'' END +
                               CASE WHEN sw.StationWorkerID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден организатор ГИА (' + CAST(ISNULL(sws.StationWorkerID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END +
                               CASE WHEN s.SubjectCode IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден предмет (' + CAST(ISNULL(sws.SubjectCode, N'NULL') AS NVARCHAR(10)) + N')' ELSE N'' END
                         FROM loader.rbd_StationWorkersSubjects AS sws
                         OUTER APPLY (SELECT r.REGION
                                        FROM dbo.rbdc_Regions AS r
                                       WHERE r.REGION = sws.REGION) AS r
                         OUTER APPLY (SELECT sw.StationWorkerID
                                        FROM rbd_StationWorkers AS sw
                                       WHERE sw.StationWorkerID = sws.StationWorkerID) AS sw
                         OUTER APPLY (SELECT s.SubjectCode
                                        FROM dat_Subjects AS s
                                       WHERE s.SubjectCode = sws.SubjectCode) AS s
                         WHERE r.REGION IS NULL OR
                               sw.StationWorkerID IS NULL OR
                               s.SubjectCode IS NULL
                OPEN swcur
                FETCH NEXT FROM swcur
                 INTO @info
                WHILE @@FETCH_STATUS = 0 AND @crow < @errors_show
                BEGIN
                    RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                    FETCH NEXT FROM swcur
                     INTO @info
                    SET @crow = @crow + 1
                END
                CLOSE swcur
                DEALLOCATE swcur
                SET @rows_count = @rows_count - @errors_show
                RAISERROR (N'+ %d ошибок синхронизации предметной специализации организаторов ГИА!', 8, 8, @rows_count ) WITH NOWAIT;
            END

            SET @crow = @nwd_count - @crow - @rows_count
            --RAISERROR ('Предметных специализаций организаторов без ошибок: %d (%d)...', 0, 0, @crow, @nwd_count) WITH NOWAIT;
            IF @crow > 0
            BEGIN
                SET @start_date = GETDATE()
                RAISERROR ('Загрузка предметной специализации организаторов ГИА...', 0, 0) WITH NOWAIT;
                MERGE dbo.rbd_StationWorkersSubjects AS tgt
                USING (SELECT sws.REGION, sws.ID, sws.SubjectCode, sws.StationWorkerID, sws.IsDeleted
                         FROM loader.rbd_StationWorkersSubjects AS sws
                        WHERE EXISTS(SELECT r.REGION
                                      FROM dbo.rbdc_Regions AS r
                                     WHERE r.REGION = sws.REGION) AND
                             EXISTS(SELECT sw.StationWorkerID
                                      FROM rbd_StationWorkers AS sw
                                     WHERE sw.StationWorkerID = sws.StationWorkerID) AND
                             EXISTS(SELECT s.SubjectCode
                                      FROM dat_Subjects AS s
                                     WHERE s.SubjectCode = sws.SubjectCode)

                     )
                   AS src
                   ON src.ID = tgt.ID
                 WHEN NOT MATCHED
                 THEN INSERT (REGION, ID, SubjectCode, StationWorkerID, IsDeleted)
                      VALUES (src.REGION, src.ID, src.SubjectCode, src.StationWorkerID, src.IsDeleted)
                WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                                  tgt.SubjectCode <> src.SubjectCode OR
                                  tgt.StationWorkerID <> src.StationWorkerID OR
                                  tgt.IsDeleted <> src.IsDeleted)
                THEN UPDATE SET REGION = src.REGION,
                                SubjectCode = src.SubjectCode,
                                StationWorkerID = src.StationWorkerID,
                                IsDeleted = src.IsDeleted
                WHEN NOT MATCHED BY SOURCE
                THEN UPDATE SET IsDeleted = 1;

                SET @rows_count = @@ROWCOUNT;
                SET @end_date = GETDATE()
                SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
                RAISERROR (N'Загрузка предметной специализации организаторов ГИА (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        END
        ELSE
            RAISERROR (N'Нет новых данных о предметных специализациях организаторов ГИА...', 0, 0) WITH NOWAIT;

        -- Загрузка сведений об аккредитации в качестве общественного наблюдателя
        SELECT @nwd_count = COUNT(src.SWorkerAccreditationID)
         FROM loader.rbd_StationWorkersAccreditation AS src
        WHERE NOT EXISTS(SELECT tgt.SWorkerAccreditationID FROM dbo.rbd_StationWorkersAccreditation AS tgt
                          WHERE src.SWorkerAccreditationID = tgt.SWorkerAccreditationID) OR
              EXISTS(SELECT tgt.SWorkerAccreditationID FROM dbo.rbd_StationWorkersAccreditation AS tgt
                      WHERE src.SWorkerAccreditationID = tgt.SWorkerAccreditationID AND
                            (tgt.Region <> src.Region OR
                             tgt.StationWorkerID <> src.StationWorkerID OR
                             tgt.GovernmentID <> src.GovernmentID OR
                             tgt.NotGovernmentAccreditation <> src.NotGovernmentAccreditation OR
                             tgt.DocumentNumber <> src.DocumentNumber OR
                             tgt.IsFamily <> src.IsFamily OR
                             tgt.DateFrom <> src.DateFrom OR
                             tgt.DateTo <> src.DateTo)
                    )
        IF @nwd_count > 0
        BEGIN
            SET @rows_count = 0;
            SET @crow = 0;
            SELECT @rows_count = COUNT(swa.SWorkerAccreditationID)
             FROM loader.rbd_StationWorkersAccreditation AS swa
            WHERE NOT EXISTS(SELECT r.REGION
                               FROM dbo.rbdc_Regions AS r
                              WHERE r.REGION = swa.REGION) OR
                  EXISTS(SELECT sw.StationWorkerID
                           FROM rbd_StationWorkers AS sw
                          WHERE sw.StationWorkerID = swa.StationWorkerID)

            IF @rows_count > 0
            BEGIN
                DECLARE swcur INSENSITIVE CURSOR
                    FOR SELECT 'Для аккредитации в качестве общественного наблюдателя ID: {' + CAST(swa.SWorkerAccreditationID AS NVARCHAR(40)) + N'}:' +
                               CASE WHEN r.REGION IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден регион (' + CAST(ISNULL(swa.REGION, N'NULL') AS NVARCHAR(3)) + N')' ELSE N'' END +
                               CASE WHEN sw.StationWorkerID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден организатор ГИА (' + CAST(ISNULL(swa.StationWorkerID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END
                         FROM loader.rbd_StationWorkersAccreditation AS swa
                         OUTER APPLY (SELECT r.REGION
                                        FROM dbo.rbdc_Regions AS r
                                       WHERE r.REGION = swa.REGION) AS r
                         OUTER APPLY (SELECT sw.StationWorkerID
                                        FROM rbd_StationWorkers AS sw
                                       WHERE sw.StationWorkerID = swa.StationWorkerID) AS sw
                         WHERE r.REGION IS NULL OR
                               sw.StationWorkerID IS NULL
                OPEN swcur
                FETCH NEXT FROM swcur
                 INTO @info
                WHILE @@FETCH_STATUS = 0 AND @crow < @errors_show
                BEGIN
                    RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                    FETCH NEXT FROM swcur
                     INTO @info
                    SET @crow = @crow + 1
                END
                CLOSE swcur
                DEALLOCATE swcur
                SET @rows_count = @rows_count - @errors_show
                RAISERROR (N'+ %d ошибок синхронизации об аккредитаций в качестве общественного наблюдателя!', 8, 8, @rows_count ) WITH NOWAIT;
            END

            SET @crow = @nwd_count - @crow - @rows_count
            --RAISERROR ('Аккредитаций в качестве общественного наблюдателя без ошибок: %d (%d)...', 0, 0, @crow, @nwd_count) WITH NOWAIT;
            IF @crow > 0
            BEGIN
                SET @start_date = GETDATE()
                RAISERROR ('Загрузка сведений об аккредитации в качестве общественного наблюдателя...', 0, 0) WITH NOWAIT;
                MERGE dbo.rbd_StationWorkersAccreditation AS tgt
                USING (SELECT swa.SWorkerAccreditationID,  swa.Region, swa.StationWorkerID, swa.GovernmentID,
                              swa.NotGovernmentAccreditation, swa.DocumentNumber, swa.IsFamily, swa.DateFrom,
                              swa.DateTo, swa.CreateDate, swa.UpdateDate, swa.ImportCreateDate, swa.ImportUpdateDate
                     FROM loader.rbd_StationWorkersAccreditation AS swa
                     WHERE EXISTS(SELECT r.REGION
                                    FROM dbo.rbdc_Regions AS r
                                   WHERE r.REGION = swa.REGION) AND
                           EXISTS(SELECT sw.StationWorkerID
                                    FROM rbd_StationWorkers AS sw
                                   WHERE sw.StationWorkerID = swa.StationWorkerID)
                     )
                   AS src
                   ON src.SWorkerAccreditationID = tgt.SWorkerAccreditationID
                 WHEN NOT MATCHED
                 THEN INSERT (SWorkerAccreditationID, Region, StationWorkerID, GovernmentID,
                              NotGovernmentAccreditation, DocumentNumber, IsFamily, DateFrom,
                              DateTo, CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate)
                      VALUES (src.SWorkerAccreditationID, src.Region, src.StationWorkerID, src.GovernmentID,
                              src.NotGovernmentAccreditation, src.DocumentNumber, src.IsFamily, src.DateFrom,
                              src.DateTo, src.CreateDate, src.UpdateDate, src.ImportCreateDate, src.ImportUpdateDate)
                WHEN MATCHED AND (tgt.Region <> src.Region OR
                                  tgt.StationWorkerID <> src.StationWorkerID OR
                                  tgt.GovernmentID <> src.GovernmentID OR
                                  tgt.NotGovernmentAccreditation <> src.NotGovernmentAccreditation OR
                                  tgt.DocumentNumber <> src.DocumentNumber OR
                                  tgt.IsFamily <> src.IsFamily OR
                                  tgt.DateFrom <> src.DateFrom OR
                                  tgt.DateTo <> src.DateTo
                                  --tgt.CreateDate <> src.CreateDate OR
                                  --tgt.UpdateDate <> src.UpdateDate OR
                                  --tgt.ImportCreateDate <> src.ImportCreateDate OR
                                  --tgt.ImportUpdateDate <> src.ImportUpdateDate
                                  )
                THEN UPDATE SET Region = src.Region,
                                StationWorkerID = src.StationWorkerID,
                                GovernmentID = src.GovernmentID,
                                NotGovernmentAccreditation = src.NotGovernmentAccreditation,
                                DocumentNumber = src.DocumentNumber,
                                IsFamily = src.IsFamily,
                                DateFrom = src.DateFrom,
                                DateTo = src.DateTo,
                                --CreateDate = src.CreateDate,
                                UpdateDate = GETDATE(),
                                --ImportCreateDate = src.ImportCreateDate,
                                ImportUpdateDate = GETDATE();
                --WHEN NOT MATCHED BY SOURCE
                --THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

                SET @rows_count = @@ROWCOUNT;
                SET @end_date = GETDATE()
                SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
                RAISERROR (N'Загрузка сведений об аккредитации в качестве общественного наблюдателя (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        END
        ELSE
            RAISERROR (N'Нет новых данных об аккредитациях в качестве общественного наблюдателя...', 0, 0) WITH NOWAIT;


        -- Загрузка распределения организаторов ГИА по ППЭ
        SELECT @nwd_count = COUNT(src.StationWorkerOnStationID)
         FROM loader.rbd_StationWorkerOnStation AS src
        WHERE NOT EXISTS(SELECT tgt.StationWorkerOnStationID FROM dbo.rbd_StationWorkerOnStation AS tgt
                          WHERE src.StationWorkerOnStationID = tgt.StationWorkerOnStationID) OR
              EXISTS(SELECT tgt.StationWorkerOnStationID FROM dbo.rbd_StationWorkerOnStation AS tgt
                      WHERE src.StationWorkerOnStationID = tgt.StationWorkerOnStationID AND
                            (tgt.StationId <> src.StationId OR
                             tgt.StationWorkerId <> src.StationWorkerId OR
                             tgt.WorkerType <> src.WorkerType OR
                             tgt.SWorkerPositionID <> src.SWorkerPositionID OR
                             tgt.Region <> src.Region OR
                             tgt.IsDeleted <> src.IsDeleted)
                    )
        IF @nwd_count > 0
        BEGIN
            SET @rows_count = 0;
            SET @crow = 0;
            SELECT @rows_count = COUNT(swos.StationWorkerOnStationID)
              FROM loader.rbd_StationWorkerOnStation AS swos
             WHERE NOT EXISTS(SELECT REGION
                                FROM dbo.rbdc_Regions AS r
                               WHERE r.REGION = swos.REGION) OR
                   NOT EXISTS(SELECT sw.StationWorkerID
                                FROM rbd_StationWorkers AS sw
                               WHERE sw.StationWorkerID = swos.StationWorkerID) OR
                   NOT EXISTS(SELECT s.StationID
                                FROM rbd_Stations AS s
                               WHERE s.StationID = swos.StationID) OR
                   NOT EXISTS(SELECT SWorkerPositionID
                                FROM rbdc_SWorkerPositions AS swp
                               WHERE swp.SWorkerPositionID = swos.SWorkerPositionID)

            IF @rows_count > 0
            BEGIN
                DECLARE swcur INSENSITIVE CURSOR
                    FOR SELECT 'Для распределения организаторов ГИА по ППЭ ID: {' + CAST(swos.StationWorkerOnStationID AS NVARCHAR(40)) + N'}:' +
                               CASE WHEN r.REGION IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден регион (' + CAST(ISNULL(swos.REGION, N'NULL') AS NVARCHAR(3)) + N')' ELSE N'' END +
                               CASE WHEN sw.StationWorkerID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден организатор ГИА (' + CAST(ISNULL(swos.StationWorkerID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END +
                               CASE WHEN s.StationID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден ППЭ (' + CAST(ISNULL(swos.StationID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END +
                               CASE WHEN swp.SWorkerPositionID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не верная должность работника (' + CAST(ISNULL(swos.SWorkerPositionID, N'NULL') AS NVARCHAR(10)) + N')' ELSE N'' END
                         FROM loader.rbd_StationWorkerOnStation AS swos
                         OUTER APPLY (SELECT REGION
                                        FROM dbo.rbdc_Regions AS r
                                       WHERE r.REGION = swos.REGION) AS r
                         OUTER APPLY (SELECT sw.StationWorkerID
                                        FROM rbd_StationWorkers AS sw
                                       WHERE sw.StationWorkerID = swos.StationWorkerID) AS sw
                         OUTER APPLY (SELECT s.StationID
                                        FROM rbd_Stations AS s
                                       WHERE s.StationID = swos.StationID) AS s
                         OUTER APPLY (SELECT swp.SWorkerPositionID
                                        FROM rbdc_SWorkerPositions AS swp
                                       WHERE swp.SWorkerPositionID = swos.SWorkerPositionID) AS swp
                         WHERE r.REGION IS NULL OR
                               sw.StationWorkerID IS NULL OR
                               s.StationID IS NULL OR
                               swp.SWorkerPositionID IS NULL
                OPEN swcur
                FETCH NEXT FROM swcur
                 INTO @info
                WHILE @@FETCH_STATUS = 0 AND @crow < @errors_show
                BEGIN
                    RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                    FETCH NEXT FROM swcur
                     INTO @info
                    SET @crow = @crow + 1
                END
                CLOSE swcur
                DEALLOCATE swcur
                SET @rows_count = @rows_count - @errors_show
                RAISERROR (N'+ %d ошибок синхронизации распределения организаторов ГИА по ППЭ!', 8, 8, @rows_count ) WITH NOWAIT;
            END

            SET @crow = @nwd_count - @crow - @rows_count
            --RAISERROR ('Предметных специализаций организаторов без ошибок: %d (%d)...', 0, 0, @crow, @nwd_count) WITH NOWAIT;
            IF @crow > 0
            BEGIN
                SET @start_date = GETDATE()
                RAISERROR ('Загрузка распределения организаторов ГИА по ППЭ...', 0, 0) WITH NOWAIT;
                MERGE dbo.rbd_StationWorkerOnStation AS tgt
                USING (SELECT swos.StationWorkerOnStationID, swos.StationId, swos.StationWorkerId,
                              swos.WorkerType, swos.SWorkerPositionID, swos.Region, swos.CreateDate,
                              swos.UpdateDate, swos.ImportCreateDate, swos.ImportUpdateDate, swos.IsDeleted
                     FROM loader.rbd_StationWorkerOnStation AS swos
                     WHERE EXISTS(SELECT REGION
                                        FROM dbo.rbdc_Regions AS r
                                       WHERE r.REGION = swos.REGION) AND
                           EXISTS(SELECT sw.StationWorkerID
                                    FROM rbd_StationWorkers AS sw
                                   WHERE sw.StationWorkerID = swos.StationWorkerID) AND
                           EXISTS(SELECT s.StationID
                                    FROM rbd_Stations AS s
                                   WHERE s.StationID = swos.StationID) AND
                           EXISTS(SELECT SWorkerPositionID
                                    FROM rbdc_SWorkerPositions AS swp
                                   WHERE swp.SWorkerPositionID = swos.SWorkerPositionID)
                     )
                   AS src
                   ON src.StationWorkerOnStationID = tgt.StationWorkerOnStationID
                 WHEN NOT MATCHED
                 THEN INSERT (StationWorkerOnStationID, StationId, StationWorkerId, WorkerType, SWorkerPositionID,
                              Region, CreateDate, UpdateDate, ImportCreateDate, ImportUpdateDate, IsDeleted)
                      VALUES (src.StationWorkerOnStationID, src.StationId, src.StationWorkerId, src.WorkerType,
                              src.SWorkerPositionID, src.Region, src.CreateDate, src.UpdateDate, src.ImportCreateDate,
                              src.ImportUpdateDate, src.IsDeleted)
                WHEN MATCHED AND (tgt.StationId <> src.StationId OR
                                  tgt.StationWorkerId <> src.StationWorkerId OR
                                  tgt.WorkerType <> src.WorkerType OR
                                  tgt.SWorkerPositionID <> src.SWorkerPositionID OR
                                  tgt.Region <> src.Region OR
                                  --tgt.CreateDate <> src.CreateDate OR
                                  --tgt.UpdateDate <> src.UpdateDate OR
                                  --tgt.ImportCreateDate <> src.ImportCreateDate OR
                                  --tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                                  tgt.IsDeleted <> src.IsDeleted)
                THEN UPDATE SET StationId = src.StationId,
                                StationWorkerId = src.StationWorkerId,
                                WorkerType = src.WorkerType,
                                SWorkerPositionID = src.SWorkerPositionID,
                                Region = src.Region,
                                --CreateDate = src.CreateDate,
                                UpdateDate = GETDATE(),
                                --ImportCreateDate = src.ImportCreateDate,
                                ImportUpdateDate = GETDATE(),
                                IsDeleted = src.IsDeleted
                WHEN NOT MATCHED BY SOURCE
                THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

                SET @rows_count = @@ROWCOUNT;
                SET @end_date = GETDATE()
                SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
                RAISERROR (N'Загрузка распределения организаторов ГИА по экзаменам в ППЭ (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        END
        ELSE
            RAISERROR (N'Нет новых данных об аккредитациях в качестве общественного наблюдателя...', 0, 0) WITH NOWAIT;

        -- Загрузка распределения организаторов ГИА по экзаменам в ППЭ
        SELECT @nwd_count = COUNT(src.StationWorkerOnExamID)
         FROM loader.rbd_StationWorkerOnExam AS src
        WHERE NOT EXISTS(SELECT tgt.StationWorkerOnExamID FROM dbo.rbd_StationWorkerOnExam AS tgt
                          WHERE src.StationWorkerOnExamID = tgt.StationWorkerOnExamID) OR
              EXISTS(SELECT tgt.StationWorkerOnExamID FROM dbo.rbd_StationWorkerOnExam AS tgt
                      WHERE src.StationWorkerOnExamID = tgt.StationWorkerOnExamID AND
                            (tgt.SWorkerPositionID <> src.SWorkerPositionID OR
                             tgt.StationsExamsID <> src.StationsExamsID OR
                             tgt.AuditoriumID <> src.AuditoriumID OR
                             tgt.StationExamAuditoryID <> src.StationExamAuditoryID OR
                             tgt.StationWorkerOnStationID <> src.StationWorkerOnStationID OR
                             tgt.StationId <> src.StationId OR
                             tgt.StationWorkerId <> src.StationWorkerId OR
                             tgt.IsDeleted <> src.IsDeleted OR
                             tgt.OrganizationRolesID <> src.OrganizationRolesID OR
                             tgt.SWorkerRoleID <> src.SWorkerRoleID)
                    )
        IF @nwd_count > 0
        BEGIN
            SET @rows_count = 0;
            SET @crow = 0;
            SELECT @rows_count = COUNT(swoe.StationWorkerOnExamID)
              FROM loader.rbd_StationWorkerOnExam AS swoe
             WHERE NOT EXISTS(SELECT REGION
                                FROM dbo.rbdc_Regions AS r
                               WHERE r.REGION = swoe.REGION) OR
                   NOT EXISTS(SELECT SWorkerPositionID
                                FROM rbdc_SWorkerPositions AS swp
                               WHERE swp.SWorkerPositionID = swoe.SWorkerPositionID) OR
                   NOT EXISTS(SELECT s.StationID
                                FROM rbd_Stations AS s
                               WHERE s.StationID = swoe.StationID) OR
                   NOT EXISTS(SELECT sw.StationWorkerID
                                FROM rbd_StationWorkers AS sw
                               WHERE sw.StationWorkerID = swoe.StationWorkerID) OR
                   NOT EXISTS(SELECT swos.StationWorkerOnStationID
                                FROM dbo.rbd_StationWorkerOnStation AS swos
                               WHERE swos.StationWorkerOnStationID = swoe.StationWorkerOnStationID)

            IF @rows_count > 0
            BEGIN
                DECLARE swcur INSENSITIVE CURSOR
                    FOR SELECT 'Для распределения организаторов ГИА по экзаменам в ППЭ ID: {' + CAST(swoe.StationWorkerOnExamID AS NVARCHAR(40)) + N'}:' +
                               CASE WHEN r.REGION IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден регион (' + CAST(ISNULL(swoe.REGION, N'NULL') AS NVARCHAR(3)) + N')' ELSE N'' END +
                               CASE WHEN sw.StationWorkerID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден организатор ГИА (' + CAST(ISNULL(swoe.StationWorkerID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END +
                               CASE WHEN s.StationID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найден ППЭ (' + CAST(ISNULL(swoe.StationID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END +
                               CASE WHEN swp.SWorkerPositionID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    неверная должность работника (' + CAST(ISNULL(swoe.SWorkerPositionID, N'NULL') AS NVARCHAR(10)) + N')' ELSE N'' END +
                               CASE WHEN se.StationsExamsID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найдено назначение ППЭ на экзамен (' + CAST(ISNULL(swoe.StationsExamsID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END +
                               CASE WHEN swos.StationWorkerOnStationID IS NULL THEN NCHAR(13) + NCHAR(10) + N'    не найдено прикрепление работника к ППЭ (' + CAST(ISNULL(swoe.StationWorkerOnStationID, N'NULL') AS NVARCHAR(40)) + N')' ELSE N'' END 
                         FROM loader.rbd_StationWorkerOnExam AS swoe
                         OUTER APPLY (SELECT REGION
                                        FROM dbo.rbdc_Regions AS r
                                       WHERE r.REGION = swoe.REGION) AS r
                         OUTER APPLY (SELECT swp.SWorkerPositionID
                                        FROM rbdc_SWorkerPositions AS swp
                                       WHERE swp.SWorkerPositionID = swoe.SWorkerPositionID) AS swp
                         OUTER APPLY (SELECT s.StationID
                                        FROM rbd_Stations AS s
                                       WHERE s.StationID = swoe.StationID) AS s
                         OUTER APPLY (SELECT sw.StationWorkerID
                                        FROM rbd_StationWorkers AS sw
                                       WHERE sw.StationWorkerID = swoe.StationWorkerID) AS sw
                         OUTER APPLY (SELECT se.StationsExamsID
                                        FROM dbo.rbd_StationsExams AS se
                                       WHERE se.StationsExamsID = swoe.StationsExamsID) AS se
                         OUTER APPLY (SELECT swos.StationWorkerOnStationID
                                        FROM dbo.rbd_StationWorkerOnStation AS swos
                                       WHERE swos.StationWorkerOnStationID = swoe.StationWorkerOnStationID) AS swos
                         WHERE r.REGION IS NULL OR
                               sw.StationWorkerID IS NULL OR
                               s.StationID IS NULL OR
                               swp.SWorkerPositionID IS NULL OR
                               se.StationsExamsID IS NULL OR
                               swos.StationWorkerOnStationID IS NULL

                OPEN swcur
                FETCH NEXT FROM swcur
                 INTO @info
                WHILE @@FETCH_STATUS = 0 AND @crow < @errors_show
                BEGIN
                    RAISERROR (N'%s!', 8, 8, @info ) WITH NOWAIT;
                    FETCH NEXT FROM swcur
                     INTO @info
                    SET @crow = @crow + 1
                END
                CLOSE swcur
                DEALLOCATE swcur
                SET @rows_count = @rows_count - @errors_show
                RAISERROR (N'+ %d ошибок синхронизации распределения организаторов ГИА по ППЭ!', 8, 8, @rows_count ) WITH NOWAIT;
            END

            SET @crow = @nwd_count - @crow - @rows_count
            --RAISERROR ('Предметных специализаций организаторов без ошибок: %d (%d)...', 0, 0, @crow, @nwd_count) WITH NOWAIT;
            IF @crow > 0
            BEGIN
                SET @start_date = GETDATE()
                RAISERROR ('Загрузка распределения организаторов ГИА по экзаменам в ППЭ...', 0, 0) WITH NOWAIT;
                MERGE dbo.rbd_StationWorkerOnExam AS tgt
                USING (SELECT swoe.REGION, swoe.StationWorkerOnExamID, swoe.SWorkerPositionID, swoe.StationsExamsID,
                              swoe.AuditoriumID, swoe.StationExamAuditoryID, swoe.StationWorkerOnStationID,
                              swoe.StationId, swoe.StationWorkerId, swoe.CreateDate, swoe.UpdateDate,
                              swoe.ImportCreateDate, swoe.ImportUpdateDate, swoe.IsDeleted, swoe.OrganizationRolesID,
                              swoe.SWorkerRoleID
                     FROM loader.rbd_StationWorkerOnExam AS swoe
                     WHERE EXISTS(SELECT REGION
                                    FROM dbo.rbdc_Regions AS r
                                   WHERE r.REGION = swoe.REGION) AND
                           EXISTS(SELECT SWorkerPositionID
                                    FROM rbdc_SWorkerPositions AS swp
                                   WHERE swp.SWorkerPositionID = swoe.SWorkerPositionID) AND
                           EXISTS(SELECT s.StationID
                                    FROM rbd_Stations AS s
                                   WHERE s.StationID = swoe.StationID) AND
                           EXISTS(SELECT sw.StationWorkerID
                                    FROM rbd_StationWorkers AS sw
                                   WHERE sw.StationWorkerID = swoe.StationWorkerID) AND
                           EXISTS(SELECT swos.StationWorkerOnStationID
                                    FROM dbo.rbd_StationWorkerOnStation AS swos
                                   WHERE swos.StationWorkerOnStationID = swoe.StationWorkerOnStationID)
                     )
                   AS src
                   ON src.StationWorkerOnExamID = tgt.StationWorkerOnExamID AND
                      src.REGION = tgt.REGION
                 WHEN NOT MATCHED
                 THEN INSERT (REGION, StationWorkerOnExamID, SWorkerPositionID, StationsExamsID,
                              AuditoriumID, StationExamAuditoryID, StationWorkerOnStationID,
                              StationId, StationWorkerId, CreateDate, UpdateDate,
                              ImportCreateDate, ImportUpdateDate, IsDeleted, OrganizationRolesID,
                              SWorkerRoleID)
                      VALUES (src.REGION, src.StationWorkerOnExamID, src.SWorkerPositionID, src.StationsExamsID,
                              src.AuditoriumID, src.StationExamAuditoryID, src.StationWorkerOnStationID,
                              src.StationId, src.StationWorkerId, src.CreateDate, src.UpdateDate,
                              src.ImportCreateDate, src.ImportUpdateDate, src.IsDeleted, src.OrganizationRolesID,
                              src.SWorkerRoleID)
                WHEN MATCHED AND (tgt.SWorkerPositionID <> src.SWorkerPositionID OR
                                  tgt.StationsExamsID <> src.StationsExamsID OR
                                  tgt.AuditoriumID <> src.AuditoriumID OR
                                  tgt.StationExamAuditoryID <> src.StationExamAuditoryID OR
                                  tgt.StationWorkerOnStationID <> src.StationWorkerOnStationID OR
                                  tgt.StationId <> src.StationId OR
                                  tgt.StationWorkerId <> src.StationWorkerId OR
                                  --tgt.CreateDate <> src.CreateDate OR
                                  --tgt.UpdateDate <> src.UpdateDate OR
                                  --tgt.ImportCreateDate <> src.ImportCreateDate OR
                                  --tgt.ImportUpdateDate <> src.ImportUpdateDate OR
                                  tgt.IsDeleted <> src.IsDeleted OR
                                  tgt.OrganizationRolesID <> src.OrganizationRolesID OR
                                  tgt.SWorkerRoleID <> src.SWorkerRoleID)
                    THEN UPDATE SET SWorkerPositionID = src.SWorkerPositionID,
                                    StationsExamsID = src.StationsExamsID,
                                    AuditoriumID = src.AuditoriumID,
                                    StationExamAuditoryID = src.StationExamAuditoryID,
                                    StationWorkerOnStationID = src.StationWorkerOnStationID,
                                    StationId = src.StationId,
                                    StationWorkerId = src.StationWorkerId,
                                    --CreateDate = src.CreateDate,
                                    UpdateDate = GETDATE(),
                                    --ImportCreateDate = src.ImportCreateDate,
                                    ImportUpdateDate = GETDATE(),
                                    IsDeleted = src.IsDeleted,
                                    OrganizationRolesID = src.OrganizationRolesID,
                                    SWorkerRoleID = src.SWorkerRoleID
                WHEN NOT MATCHED BY SOURCE
                THEN UPDATE SET IsDeleted = 1, UpdateDate = GETDATE(), ImportUpdateDate = GETDATE();

                SET @rows_count = @@ROWCOUNT;
                SET @end_date = GETDATE()
                SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
                RAISERROR (N'Загрузка распределения организаторов ГИА по экзаменам в ППЭ (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
            END
        END
        ELSE
            RAISERROR (N'Нет новых данных распределения организаторов ГИА по экзаменам в ППЭ...', 0, 0) WITH NOWAIT;
        --===============================================================================--
        -- Блок первичной обработки бланков
        IF OBJECT_ID('dbo.sht_Packages') IS NOT NULL
        BEGIN
            -- Загрузка информации о пакетах
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка информации о пакетах...', 0, 0) WITH NOWAIT;

            MERGE dbo.sht_Packages AS tgt
            USING (SELECT p.REGION, p.PackageID, p.[FileName], p.RegionCode, p.DepartmentCode,
                          p.TestTypeCode, p.SubjectCode, p.ExamDate, p.StationCode, p.AuditoriumCode,
                          p.Condition, p.CreateTime, p.UpdateTime, p.SheetsCount, p.IsExported,
                          p.ProjectName, p.ProjectBatchID, p.ProjectBatchName
                     FROM loader.sht_Packages AS p
                    WHERE EXISTS(SELECT ID FROM rbd_CurrentRegion AS cr
                                  WHERE p.REGION = cr.REGION))
               AS src
               ON src.PackageID = tgt.PackageID AND
                  src.Region = tgt.Region
            WHEN NOT MATCHED
            THEN INSERT (REGION, PackageID, [FileName], RegionCode, DepartmentCode, TestTypeCode,
                         SubjectCode, ExamDate, StationCode, AuditoriumCode, Condition, CreateTime,
                         UpdateTime, SheetsCount, IsExported, ProjectName, ProjectBatchID, ProjectBatchName)
            VALUES (src.REGION, src.PackageID, src.[FileName], src.RegionCode, src.DepartmentCode,
                    src.TestTypeCode, src.SubjectCode, src.ExamDate, src.StationCode, src.AuditoriumCode,
                    src.Condition, src.CreateTime, src.UpdateTime, src.SheetsCount, src.IsExported,
                    src.ProjectName, src.ProjectBatchID, src.ProjectBatchName)
            WHEN MATCHED AND (tgt.[FileName] <> src.[FileName] OR
                              tgt.RegionCode <> src.RegionCode OR
                              tgt.DepartmentCode <> src.DepartmentCode OR
                              tgt.TestTypeCode <> src.TestTypeCode OR
                              tgt.SubjectCode <> src.SubjectCode OR
                              tgt.ExamDate <> src.ExamDate OR
                              tgt.StationCode <> src.StationCode OR
                              tgt.AuditoriumCode <> src.AuditoriumCode OR
                              tgt.Condition <> src.Condition OR
                              tgt.CreateTime <> src.CreateTime OR
                              tgt.UpdateTime <> src.UpdateTime OR
                              tgt.SheetsCount <> src.SheetsCount OR
                              tgt.IsExported <> src.IsExported OR
                              tgt.ProjectName <> src.ProjectName OR
                              tgt.ProjectBatchID <> src.ProjectBatchID OR
                              tgt.ProjectBatchName <> src.ProjectBatchName)
            THEN UPDATE SET [FileName] = src.[FileName],
                            RegionCode = src.RegionCode,
                            DepartmentCode = src.DepartmentCode,
                            TestTypeCode = src.TestTypeCode,
                            SubjectCode = src.SubjectCode,
                            ExamDate = src.ExamDate,
                            StationCode = src.StationCode,
                            AuditoriumCode = src.AuditoriumCode,
                            Condition = src.Condition,
                            CreateTime = src.CreateTime,
                            UpdateTime = src.UpdateTime,
                            SheetsCount = src.SheetsCount,
                            IsExported = src.IsExported,
                            ProjectName = src.ProjectName,
                            ProjectBatchID = src.ProjectBatchID,
                            ProjectBatchName = src.ProjectBatchName;
            --WHEN NOT MATCHED BY SOURCE
            --THEN UPDATE SET IsExported = 9, UpdateTime = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка информации о пакетах (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;

            -- Загрузка информации о бланках регистрации
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка информации о бланках регистрации...', 0, 0) WITH NOWAIT;

            MERGE dbo.sht_Sheets_R AS tgt
            USING (SELECT r.REGION, r.SheetID, r.PackageFK, r.[FileName], r.RegionCode, r.DepartmentCode,
                          r.TestTypeCode, r.SubjectCode, r.ExamDate, r.StationCode, r.AuditoriumCode,
                          r.Barcode, r.CRC, r.ParticipantID, r.SchoolCode, r.Surname, r.Name, r.SecondName,
                          r.DocumentSeries, r.DocumentNumber, r.DocumentHash, r.Sex, r.ImageNumber, r.VariantCode,
                          r.HasSignature, r.Condition, r.ProjectBatchID,
                          r.Reserve01, r.Reserve02, r.Reserve03, r.Reserve04, r.Reserve05,
                          r.Reserve06, r.Reserve07, r.Reserve08, r.Reserve09, r.Reserve10
                     FROM loader.sht_Sheets_R AS r
                    WHERE EXISTS(SELECT ID FROM rbd_CurrentRegion AS cr
                                  WHERE r.REGION = cr.REGION) AND
                          EXISTS(SELECT PackageID
                                   FROM dbo.sht_Packages AS p
                                  WHERE p.PackageID = r.PackageFK))
               AS src
               ON src.SheetID = tgt.SheetID
            WHEN NOT MATCHED
            THEN INSERT (REGION, SheetID, PackageFK, [FileName], RegionCode, DepartmentCode, TestTypeCode,
                         SubjectCode, ExamDate, StationCode, AuditoriumCode, Barcode, CRC, ParticipantID,
                         SchoolCode, Surname, Name, SecondName, DocumentSeries, DocumentNumber, DocumentHash,
                         Sex, ImageNumber, VariantCode, HasSignature, Condition, ProjectBatchID,
                         Reserve01, Reserve02, Reserve03, Reserve04, Reserve05,
                         Reserve06, Reserve07, Reserve08, Reserve09, Reserve10)
            VALUES (src.REGION, src.SheetID, src.PackageFK, src.[FileName], src.RegionCode, src.DepartmentCode,
                    src.TestTypeCode, src.SubjectCode, src.ExamDate, src.StationCode, src.AuditoriumCode,
                    src.Barcode, src.CRC, src.ParticipantID, src.SchoolCode, src.Surname, src.Name, src.SecondName,
                    src.DocumentSeries, src.DocumentNumber, src.DocumentHash, src.Sex, src.ImageNumber, src.VariantCode,
                    src.HasSignature, src.Condition, src.ProjectBatchID,
                    src.Reserve01, src.Reserve02, src.Reserve03, src.Reserve04, src.Reserve05,
                    src.Reserve06, src.Reserve07, src.Reserve08, src.Reserve09, src.Reserve10)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.PackageFK <> src.PackageFK OR
                              tgt.[FileName] <> src.[FileName] OR
                              tgt.RegionCode <> src.RegionCode OR
                              tgt.DepartmentCode <> src.DepartmentCode OR
                              tgt.TestTypeCode <> src.TestTypeCode OR
                              tgt.SubjectCode <> src.SubjectCode OR
                              tgt.ExamDate <> src.ExamDate OR
                              tgt.StationCode <> src.StationCode OR
                              tgt.AuditoriumCode <> src.AuditoriumCode OR
                              tgt.Barcode <> src.Barcode OR
                              tgt.CRC <> src.CRC OR
                              tgt.ParticipantID <> src.ParticipantID OR
                              tgt.SchoolCode <> src.SchoolCode OR
                              tgt.Surname <> src.Surname OR
                              tgt.Name <> src.Name OR
                              tgt.SecondName <> src.SecondName OR
                              tgt.DocumentSeries <> src.DocumentSeries OR
                              tgt.DocumentNumber <> src.DocumentNumber OR
                              tgt.DocumentHash <> src.DocumentHash OR
                              tgt.Sex <> src.Sex OR
                              tgt.ImageNumber <> src.ImageNumber OR
                              tgt.VariantCode <> src.VariantCode OR
                              tgt.HasSignature <> src.HasSignature OR
                              tgt.Condition <> src.Condition OR
                              tgt.ProjectBatchID <> src.ProjectBatchID OR
                              tgt.Reserve01 <> src.Reserve01 OR
                              tgt.Reserve02 <> src.Reserve02 OR
                              tgt.Reserve03 <> src.Reserve03 OR
                              tgt.Reserve04 <> src.Reserve04 OR
                              tgt.Reserve05 <> src.Reserve05 OR
                              tgt.Reserve06 <> src.Reserve06 OR
                              tgt.Reserve07 <> src.Reserve07 OR
                              tgt.Reserve08 <> src.Reserve08 OR
                              tgt.Reserve09 <> src.Reserve09 OR
                              tgt.Reserve10 <> src.Reserve10)
            THEN UPDATE SET REGION = src.REGION,
                            PackageFK = src.PackageFK,
                            [FileName] = src.[FileName],
                            RegionCode = src.RegionCode,
                            DepartmentCode = src.DepartmentCode,
                            TestTypeCode = src.TestTypeCode,
                            SubjectCode = src.SubjectCode,
                            ExamDate = src.ExamDate,
                            StationCode = src.StationCode,
                            AuditoriumCode = src.AuditoriumCode,
                            Barcode = src.Barcode,
                            CRC = src.CRC,
                            ParticipantID = src.ParticipantID,
                            SchoolCode = src.SchoolCode,
                            Surname = src.Surname,
                            Name = src.Name,
                            SecondName = src.SecondName,
                            DocumentSeries = src.DocumentSeries,
                            DocumentNumber = src.DocumentNumber,
                            DocumentHash = src.DocumentHash,
                            Sex = src.Sex,
                            ImageNumber = src.ImageNumber,
                            VariantCode = src.VariantCode,
                            HasSignature = src.HasSignature,
                            Condition = src.Condition,
                            ProjectBatchID = src.ProjectBatchID,
                            Reserve01 = src.Reserve01,
                            Reserve02 = src.Reserve02,
                            Reserve03 = src.Reserve03,
                            Reserve04 = src.Reserve04,
                            Reserve05 = src.Reserve05,
                            Reserve06 = src.Reserve06,
                            Reserve07 = src.Reserve07,
                            Reserve08 = src.Reserve08,
                            Reserve09 = src.Reserve09,
                            Reserve10 = src.Reserve10;
            --WHEN NOT MATCHED BY SOURCE
            --THEN UPDATE SET IsExported = 9, UpdateTime = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка информации о бланках регистрации (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;

            -- Загрузка информации о бланках №1
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка информации о бланках №1...', 0, 0) WITH NOWAIT;

            MERGE dbo.sht_Sheets_AB AS tgt
            USING (SELECT ab.REGION,  ab.SheetID, ab.PackageFK, ab.[FileName], ab.RegionCode, ab.DepartmentCode,
                          ab.TestTypeCode, ab.SubjectCode, ab.ExamDate, ab.StationCode, ab.AuditoriumCode,
                          ab.Barcode, ab.CRC, ab.ImageNumber, ab.VariantCode, ab.HasSignature, ab.Condition,
                          ab.ProjectBatchID, ab.Reserve01, ab.Reserve02, ab.Reserve03, ab.Reserve04, ab.Reserve05,
                          ab.Reserve06, ab.Reserve07, ab.Reserve08, ab.Reserve09, ab.Reserve10
                     FROM loader.sht_Sheets_AB AS ab
                    WHERE EXISTS(SELECT ID FROM rbd_CurrentRegion AS cr
                                  WHERE ab.REGION = cr.REGION) AND
                          EXISTS(SELECT PackageID
                                   FROM dbo.sht_Packages AS p
                                  WHERE p.PackageID = ab.PackageFK))
               AS src
               ON src.SheetID = tgt.SheetID
            WHEN NOT MATCHED
            THEN INSERT (REGION, SheetID, PackageFK, [FileName], RegionCode, DepartmentCode, TestTypeCode,
                         SubjectCode, ExamDate, StationCode, AuditoriumCode, Barcode, CRC, ImageNumber,
                         VariantCode, HasSignature, Condition, ProjectBatchID,
                         Reserve01, Reserve02, Reserve03, Reserve04, Reserve05,
                         Reserve06, Reserve07, Reserve08, Reserve09, Reserve10)
            VALUES (src.REGION, src.SheetID, src.PackageFK, src.[FileName], src.RegionCode, src.DepartmentCode,
                    src.TestTypeCode, src.SubjectCode, src.ExamDate, src.StationCode, src.AuditoriumCode,
                    src.Barcode, src.CRC, src.ImageNumber, src.VariantCode, src.HasSignature, src.Condition,
                    src.ProjectBatchID, src.Reserve01, src.Reserve02, src.Reserve03, src.Reserve04, src.Reserve05,
                    src.Reserve06, src.Reserve07, src.Reserve08, src.Reserve09, src.Reserve10)
            WHEN MATCHED AND (tgt.PackageFK <> src.PackageFK OR
                              tgt.[FileName] <> src.[FileName] OR
                              tgt.RegionCode <> src.RegionCode OR
                              tgt.DepartmentCode <> src.DepartmentCode OR
                              tgt.TestTypeCode <> src.TestTypeCode OR
                              tgt.SubjectCode <> src.SubjectCode OR
                              tgt.ExamDate <> src.ExamDate OR
                              tgt.StationCode <> src.StationCode OR
                              tgt.AuditoriumCode <> src.AuditoriumCode OR
                              tgt.Barcode <> src.Barcode OR
                              tgt.CRC <> src.CRC OR
                              tgt.ImageNumber <> src.ImageNumber OR
                              tgt.VariantCode <> src.VariantCode OR
                              tgt.HasSignature <> src.HasSignature OR
                              tgt.Condition <> src.Condition OR
                              tgt.ProjectBatchID <> src.ProjectBatchID OR
                              tgt.Reserve01 <> src.Reserve01 OR
                              tgt.Reserve02 <> src.Reserve02 OR
                              tgt.Reserve03 <> src.Reserve03 OR
                              tgt.Reserve04 <> src.Reserve04 OR
                              tgt.Reserve05 <> src.Reserve05 OR
                              tgt.Reserve06 <> src.Reserve06 OR
                              tgt.Reserve07 <> src.Reserve07 OR
                              tgt.Reserve08 <> src.Reserve08 OR
                              tgt.Reserve09 <> src.Reserve09 OR
                              tgt.Reserve10 <> src.Reserve10)
            THEN UPDATE SET PackageFK = src.PackageFK,
                            [FileName] = src.[FileName],
                            RegionCode = src.RegionCode,
                            DepartmentCode = src.DepartmentCode,
                            TestTypeCode = src.TestTypeCode,
                            SubjectCode = src.SubjectCode,
                            ExamDate = src.ExamDate,
                            StationCode = src.StationCode,
                            AuditoriumCode = src.AuditoriumCode,
                            Barcode = src.Barcode,
                            CRC = src.CRC,
                            ImageNumber = src.ImageNumber,
                            VariantCode = src.VariantCode,
                            HasSignature = src.HasSignature,
                            Condition = src.Condition,
                            ProjectBatchID = src.ProjectBatchID,
                            Reserve01 = src.Reserve01,
                            Reserve02 = src.Reserve02,
                            Reserve03 = src.Reserve03,
                            Reserve04 = src.Reserve04,
                            Reserve05 = src.Reserve05,
                            Reserve06 = src.Reserve06,
                            Reserve07 = src.Reserve07,
                            Reserve08 = src.Reserve08,
                            Reserve09 = src.Reserve09,
                            Reserve10 = src.Reserve10;
            --WHEN NOT MATCHED BY SOURCE
            --THEN UPDATE SET IsExported = 9, UpdateTime = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка информации о бланках №1 (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;

            -- Загрузка информации об ответах бланков №1
            SET @start_date = GETDATE()
            RAISERROR ('Загрузка информации об ответах бланков №1...', 0, 0) WITH NOWAIT;

            MERGE dbo.sht_Marks_AB AS tgt
            USING (SELECT mab.REGION, mab.MarkID, mab.SheetFK, mab.TaskTypeCode, mab.TaskNumber, mab.AnswerValue, mab.ReplaceValue
                     FROM loader.sht_Marks_AB AS mab
                    WHERE EXISTS(SELECT ID FROM rbd_CurrentRegion AS cr
                                  WHERE mab.REGION = cr.REGION) AND
                          EXISTS(SELECT ab.SheetID
                                   FROM dbo.sht_Sheets_AB AS ab
                                  WHERE ab.SheetID = mab.SheetFK))
               AS src
               ON src.MarkID = tgt.MarkID
            WHEN NOT MATCHED
            THEN INSERT (REGION, MarkID, SheetFK, TaskTypeCode, TaskNumber, AnswerValue, ReplaceValue)
            VALUES (src.REGION, src.MarkID, src.SheetFK, src.TaskTypeCode, src.TaskNumber, src.AnswerValue, src.ReplaceValue)
            WHEN MATCHED AND (tgt.REGION <> src.REGION OR
                              tgt.SheetFK <> src.SheetFK OR
                              tgt.TaskTypeCode <> src.TaskTypeCode OR
                              tgt.TaskNumber <> src.TaskNumber OR
                              tgt.AnswerValue <> src.AnswerValue OR
                              tgt.ReplaceValue<> src.ReplaceValue)
            THEN UPDATE SET REGION = src.REGION,
                            SheetFK = src.SheetFK,
                            TaskTypeCode = src.TaskTypeCode,
                            TaskNumber = src.TaskNumber,
                            AnswerValue = src.AnswerValue,
                            ReplaceValue= src.ReplaceValue;
            --WHEN NOT MATCHED BY SOURCE
            --THEN UPDATE SET IsExported = 9, UpdateTime = GETDATE();

            SET @rows_count = @@ROWCOUNT;
            SET @end_date = GETDATE()
            SET @elapsed = DATEDIFF(second, @task_start_date, @end_date);
            RAISERROR (N'Загрузка информации об ответах бланков №1 (всего: %d) завершена за %d с.!', 0, 0, @rows_count, @elapsed ) WITH NOWAIT;
        END

        --COMMIT TRANSACTION importing
        SET @end_date = GETDATE()
        PRINT 'Импорт данных завершён за ' +
              CAST(DATEDIFF(second, @start_date, @end_date) AS NVARCHAR(6)) + ' с.'
    END TRY
    BEGIN CATCH
        PRINT 'Ошибка!!!'
        --ROLLBACK TRANSACTION importing
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(),
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage,
                   @ErrorSeverity,
                   @ErrorState );
    END CATCH;
    
END

GO


