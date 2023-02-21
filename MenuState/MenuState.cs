public abstract class MenuState : IState
{
    protected abstract Dictionary<int, MenuItem> Menus { get; }

    protected virtual void ShowMenu()
    {
        Console.WriteLine("You have options:");
        foreach (var m in Menus)
            Console.WriteLine($"{m.Key} - {m.Value.Text}");
    }

    protected virtual KeyValuePair<int, MenuItem> ReadOption()
    {
        Console.WriteLine("Please, select option:");
        ShowMenu();

        var str = Console.ReadLine();
        int answerId = 0;

        if (int.TryParse(str, out answerId))
        {
            if (!Menus.ContainsKey(answerId))
            {
                Console.WriteLine("Selected item notexists.");
                return ReadOption();
            }
            return new KeyValuePair<int, MenuItem>(answerId, Menus[answerId]);
        }
        else
        {
            Console.WriteLine("Selected item not a number.");
            return ReadOption();
        }
    }

    public virtual IState RunState()
    {
        var option = ReadOption();
        return NextState(option);
    }

    protected abstract IState NextState(KeyValuePair<int, MenuItem> selectedMenu);
}