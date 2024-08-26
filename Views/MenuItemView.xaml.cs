namespace FlamingForkAdmin.Views
{
    public partial class MenuItemView : ContentView
    {
        public MenuItemView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemImageSourceProperty =
            BindableProperty.Create(nameof(ItemImageSource), typeof(string), typeof(MenuItemView));

        public string ItemImageSource
        {
            get => (string)GetValue(ItemImageSourceProperty);
            set => SetValue(ItemImageSourceProperty, value);
        }

        public static readonly BindableProperty ItemIdProperty =
            BindableProperty.Create(nameof(ItemId), typeof(int), typeof(MenuItemView));

        public int ItemId
        {
            get => (int)GetValue(ItemIdProperty);
            set => SetValue(ItemIdProperty, value);
        }

        public static readonly BindableProperty ItemNameProperty =
            BindableProperty.Create(nameof(ItemName), typeof(string), typeof(MenuItemView));

        public string ItemName
        {
            get => (string)GetValue(ItemNameProperty);
            set => SetValue(ItemNameProperty, value);
        }

        public static readonly BindableProperty ItemCategoryProperty =
            BindableProperty.Create(nameof(ItemCategory), typeof(string), typeof(MenuItemView));

        public string ItemCategory
        {
            get => (string)GetValue(ItemCategoryProperty);
            set => SetValue(ItemCategoryProperty, value);
        }

        public static readonly BindableProperty ItemPriceProperty =
            BindableProperty.Create(nameof(ItemPrice), typeof(int), typeof(MenuItemView));

        public int ItemPrice
        {
            get => (int)GetValue(ItemPriceProperty);
            set => SetValue(ItemPriceProperty, value);
        }

        public static readonly BindableProperty DeleteMenuItemCommandProperty =
            BindableProperty.Create(nameof(DeleteMenuItemCommand), typeof(Command), typeof(MenuItemView));

        public Command DeleteMenuItemCommand
        {
            get => (Command)GetValue(DeleteMenuItemCommandProperty);
            set => SetValue(DeleteMenuItemCommandProperty, value);
        }
    }
}
