namespace FtcReportControls.WindowsSystem.VistaTaskDialog
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Vista-like chevron button.
    /// </summary>
    internal partial class ChevronButton : System.Windows.Forms.Button
    {
        #region Constructor

        public ChevronButton()
        {
            InitializeComponent();
            Image = FtcReportControls.Properties.Resources.ChevronMore;
        }

        #endregion

        #region Fields & Properties

        private bool _isHovered;
        private bool _isFocused;
        private bool _isFocusedByKey;
        private bool _isKeyDown;
        private bool _isMouseDown;
        private bool _isPressed
        {
            get { return _isKeyDown || (_isMouseDown && _isHovered); }
        }

        private bool _expanded;
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                _expanded = value;
                SetImage();
            }
        }

        public override bool Focused
        {
            get { return false; }
        }

        #endregion

        #region Private Methods

        private void SetImage()
        {
            if (_isPressed)
            {
                Image = _expanded
                        ? FtcReportControls.Properties.Resources.ChevronLessPressed
                        : FtcReportControls.Properties.Resources.ChevronMorePressed;
            }
            else if (_isHovered || _isFocused)
            {
                Image = _expanded
                        ? FtcReportControls.Properties.Resources.ChevronLessHovered 
                        : FtcReportControls.Properties.Resources.ChevronMoreHovered;
            }
            else
            {
                Image = _expanded
                        ? FtcReportControls.Properties.Resources.ChevronLess
                        : FtcReportControls.Properties.Resources.ChevronMore;
            }
        }

        #endregion

        #region Overrided Methods

        protected override void OnClick(EventArgs e)
        {
            _isKeyDown = false;
            _isMouseDown = false;
            _expanded ^= true;
            SetImage();
            base.OnClick(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            _isFocused = true;
            _isFocusedByKey = true;
            SetImage();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            _isFocused = false;
            _isFocusedByKey = false;
            _isKeyDown = false;
            _isMouseDown = false;
            SetImage();
        }

        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            if (kevent.KeyCode == Keys.Space)
            {
                _isKeyDown = true;
                SetImage();
            }
            base.OnKeyDown(kevent);
        }

        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (_isKeyDown && (kevent.KeyCode == Keys.Space))
            {
                _isKeyDown = false;
                SetImage();
            }
            base.OnKeyUp(kevent);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (!_isMouseDown && (mevent.Button == MouseButtons.Left))
            {
                _isMouseDown = true;
                _isFocusedByKey = false;
                SetImage();
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if ((_isMouseDown))
            {
                _isMouseDown = false;
                SetImage();
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
            if (mevent.Button != MouseButtons.None)
            {
                if (!ClientRectangle.Contains(mevent.X, mevent.Y))
                {
                    if (_isHovered)
                    {
                        _isHovered = false;
                        SetImage();
                    }
                }
                else if (!_isHovered)
                {
                    _isHovered = true;
                    SetImage();
                }
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            SetImage();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            SetImage();
            base.OnMouseLeave(e);
        }

        #endregion
    }
}
