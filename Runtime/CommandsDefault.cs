using UnityEngine;
using Popcron.Console;

[Category("Default commands")]
public class CommandsDefault
{
    [Command("info", "Prints system information.")]
    public static void PrintSystemInfo()
    {
        string os = SystemInfo.operatingSystem;
        string cpu = SystemInfo.processorType + ", " + SystemInfo.processorCount + " cores (" + (SystemInfo.processorFrequency / 1000f) + " hz)";
        string ram = (SystemInfo.systemMemorySize / 1000f) + " gb";

        Console.Print("OS: " + os);
        Console.Print("CPU: " + cpu);
        Console.Print("RAM: " + ram);
    }

    [Command("clear", "Clears the console window.")]
    public static void ClearConsole()
    {
        Console.Clear();
        Debug.ClearDeveloperConsole();
    }

    [Command("owners", "Lists all instance command owners.")]
    public static void Owners()
    {
        foreach (Owner owner in Parser.Owners)
        {
            Console.Print("\t" + owner.id + " = " + owner.owner);
            foreach (var method in owner.methods)
            {
                Console.Print("\t\t" + method.Name);
            }
        }
    }

    [Command("converters", "Lists all type converters.")]
    public static void Converters()
    {
        foreach (Converter converter in Converter.Converters)
        {
            Console.Print("\t" + converter.GetType() + " (" + converter.Type + ")");
        }
    }

    [Command("help", "Describes a specific command.")]
    public static void HelpAgain(string name)
    {
        bool exists = false;
        foreach (var command in Library.Commands)
        {
            if (command.Name == name)
            {
                if (!exists) Console.Print("Signature for " + command.Name);
                exists = true;

                string text = "\t" + command.Name;
                foreach (var parameter in command.Parameters)
                {
                    text += " <" + parameter.Name + ">";
                }

                //add description
                if (command.Description != "")
                {
                    text += " = " + command.Description;
                }

                Console.Print(text);
            }
        }

        if (!exists) Console.Print(name + " doesn't exist.");
    }

    [Command("help", "Outputs a list of all commands")]
    public static void Help()
    {
        Console.Print("All commands registered: ");
        foreach (var category in Library.Categories)
        {
            Console.Print("\t" + category.Name);
            foreach (var command in category.Commands)
            {
                string text = string.Join("/", command.Names);
                foreach (var parameter in command.Parameters)
                {
                    text += " <" + parameter.Name + ">";
                }

                if (command.Description != "")
                {
                    text += " = " + command.Description;
                }

                if (!command.IsStatic)
                {
                    text = "@id " + text;
                }

                Console.Print("\t\t" + text);
            }
        }
    }

    [Command("echo")]
    public static void Echo(string text)
    {
        Console.Print(text);
    }

    [Command("list controllers")]
    public static void ListControllers()
    {
        var joys = Input.GetJoystickNames();
        if (joys.Length == 0)
        {
            Console.Warn("No controllers plugged in.");
        }
        else
        {
            for (int i = 0; i < joys.Length; i++)
            {
                Console.Print("\t" + joys[i]);
            }
        }
    }
}
