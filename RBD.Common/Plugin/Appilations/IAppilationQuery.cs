using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBD.Common.Plugin.Appilations
{
    public interface IAppilationQuery
    {
        void DataSet(PersonalData[] data);
    }
  
    public class AppilationPluginQuery
    {
        public const string FindPersonalData =
            @"SELECT DISTINCT
                      p.ParticipantID as Id,
                      p.Surname as Surname,
                      p.Name as Name,
                      p.SecondName as SecondName,
                      p.DocumentSeries as DocumentSeries,
                      p.DocumentNumber as DocumentNumber,
                      ht.HumanTestID AS HumanTest,
                      ht.SubjectCode AS SubjectCode,
                      ht.ExamDate AS ExamDate,
                      aa.AppealCondition,
                      ac.AppealConditionName as AppealConditionName
            FROM rbd_Participants p
            JOIN res_HumanTests AS ht ON ht.ParticipantFK = p.ParticipantID
            LEFT JOIN ac_Appeals AS aa ON aa.HumanTestFK = ht.HumanTestID AND AppealType = 1
            LEFT JOIN rbdc_AppealConditions ac ON ac.AppealConditionCode = aa.AppealCondition
                                            AND ac.AppealTypeCode = a.AppealType
            WHERE p.ParticipantID IN (:id)";

        public const string CreateApil = @"exec ftcac_CreateAppeal
                            @REGION = :REGION,
                            @Participant = :Participant,
                            @Surname = :Surname,
                            @Name = :Name,
                            @SecondName = :SecondName,
                            @DocNumber = :DocNumber,
                            @HumanTest = :HumanTest,
                            @Subject = :Subject,
                            @ExamDate = :ExamDate,
                            @AppealType = 1,
                            @CreateDate = :CreateDate,
                            @StationCode = null,
                            @AuditoriumCode = null,
                            @WorkStationCode = 200,
                            @Declined = 0,
                            @Condition = 10,
                            @Comment = ''
                        ";


        /*
             //    .ftcac_CreateAppeal(h.REGION, participantId, participant.Fio.Surname, participant.Fio.Name,
            //                        participant.Fio.SecondName, participant.Document.DocumentNumber, h.HumanTestID,
            //                        exam.SubjectCode, exam.ExamDate, null, null, 1, DateTime.Now.ToString("d"), 200, false, 10, "");
         
         */
    }
}
