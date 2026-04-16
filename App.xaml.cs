using DocArchiveAdmin.Services;

namespace DocArchiveAdmin
{
    public partial class App : Application
    {
        private readonly WindowService _windowService;

        public App(WindowService windowService)
        {
            InitializeComponent();
            _windowService = windowService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new MainPage())
            {
                Title = "DocArchiveAdmin"
            };

            window.Created += (s, e) =>
            {
                _windowService.Attach(window);

                // ONLY login mode at startup
                _windowService.SetLoginMode();
            };

            return window;
        }
    } 
}