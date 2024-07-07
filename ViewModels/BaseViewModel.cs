namespace nidirect_app_frontend.ViewModels;

public class BaseViewModel
{
    /// <summary>
    /// Gets or sets DisplayName.
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    public string SectionName { get; set; }

    /// <summary>Gets or sets the name of the title tag.</summary>
    /// <value>The name of the title tag.</value>
    public string TitleTagName { get; set; }

    /// <summary>
    /// Sets error class for form controls
    /// </summary>
    public string ErrorClass { get; set; }
}