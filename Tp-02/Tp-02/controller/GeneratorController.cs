namespace Tp_02.controller
{
    internal class Program
    {

        private FormGenerator Form;
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormGenerator());
        }
    }
}