namespace Composite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var form = new Form("login");
            form.AddComponent(new LabelText("Password"));
            form.AddComponent(new InputText("password", "12345"));
            form.AddComponent(new LabelText("Login"));
            form.AddComponent(new InputText("login", "Admin User"));

            Console.WriteLine(form.ConvertToString());
        }
    }
}
