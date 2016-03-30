namespace Conzo.Templates
{
   public interface ITemplateProvider
   {
      string GetHeader();
      string GetFooter();
      string GetRenderedTemplate(string stuff);
   }
}
