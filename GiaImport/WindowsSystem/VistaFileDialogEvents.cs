using System;
using System.Collections.Generic;
using System.Text;

namespace FtcReportControls.WindowsSystem
{
    class VistaFileDialogEvents : FtcReportControls.WindowsSystem.Interop.IFileDialogEvents, FtcReportControls.WindowsSystem.Interop.IFileDialogControlEvents
    {
        const uint S_OK = 0;
        const uint S_FALSE = 1;
        const uint E_NOTIMPL = 0x80004001;

        private VistaFileDialog _dialog;

        public VistaFileDialogEvents(VistaFileDialog dialog)
        {
            if( dialog == null )
                throw new ArgumentNullException("dialog");

            _dialog = dialog;
        }

        #region IFileDialogEvents Members

        public Interop.HRESULT OnFileOk(FtcReportControls.WindowsSystem.Interop.IFileDialog pfd)
        {
            if( _dialog.DoFileOk(pfd) )
                return FtcReportControls.WindowsSystem.Interop.HRESULT.S_OK;
            else
                return FtcReportControls.WindowsSystem.Interop.HRESULT.S_FALSE;
        }

        public Interop.HRESULT OnFolderChanging(FtcReportControls.WindowsSystem.Interop.IFileDialog pfd, FtcReportControls.WindowsSystem.Interop.IShellItem psiFolder)
        {
            return FtcReportControls.WindowsSystem.Interop.HRESULT.S_OK;
        }

        public void OnFolderChange(FtcReportControls.WindowsSystem.Interop.IFileDialog pfd)
        {
        }

        public void OnSelectionChange(FtcReportControls.WindowsSystem.Interop.IFileDialog pfd)
        {
        }

        public void OnShareViolation(FtcReportControls.WindowsSystem.Interop.IFileDialog pfd, FtcReportControls.WindowsSystem.Interop.IShellItem psi, out NativeMethods.FDE_SHAREVIOLATION_RESPONSE pResponse)
        {
            pResponse = NativeMethods.FDE_SHAREVIOLATION_RESPONSE.FDESVR_DEFAULT;
        }

        public void OnTypeChange(FtcReportControls.WindowsSystem.Interop.IFileDialog pfd)
        {
        }

        public void OnOverwrite(FtcReportControls.WindowsSystem.Interop.IFileDialog pfd, FtcReportControls.WindowsSystem.Interop.IShellItem psi, out NativeMethods.FDE_OVERWRITE_RESPONSE pResponse)
        {
            pResponse = NativeMethods.FDE_OVERWRITE_RESPONSE.FDEOR_DEFAULT;
        }

        #endregion

        #region IFileDialogControlEvents Members

        public void OnItemSelected(FtcReportControls.WindowsSystem.Interop.IFileDialogCustomize pfdc, int dwIDCtl, int dwIDItem)
        {
        }

        public void OnButtonClicked(FtcReportControls.WindowsSystem.Interop.IFileDialogCustomize pfdc, int dwIDCtl)
        {
            if( dwIDCtl == VistaFileDialog.HelpButtonId )
                _dialog.DoHelpRequest();
        }

        public void OnCheckButtonToggled(FtcReportControls.WindowsSystem.Interop.IFileDialogCustomize pfdc, int dwIDCtl, bool bChecked)
        {
        }

        public void OnControlActivating(FtcReportControls.WindowsSystem.Interop.IFileDialogCustomize pfdc, int dwIDCtl)
        {
        }

        #endregion


    }
}
