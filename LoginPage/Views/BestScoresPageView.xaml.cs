namespace LoginPage.Views;
using ViewModels;

public partial class BestScoresPageView : ContentPage
{
	public BestScoresPageView(BestScoresPageViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
		
    }
}