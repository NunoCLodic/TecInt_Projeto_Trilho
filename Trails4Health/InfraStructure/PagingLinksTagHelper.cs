using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Trails4Health.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ver _ViewImports.
namespace Trails4Health.Infrastructure
{

    // isto é um tag helper Ex: asp-action
    // vai-me gerar os botões e os links de pag. para pagina, onde vou aplicar o tag helper(div) 
    // Attributes > page-model == PageModel
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PagingLinksTagHelper : TagHelper {

        // vai-me gerar os links da paginação
        private IUrlHelperFactory urlHelperFactory; 
        // links antes e dps pag. atual
        public static int MaxLinksBeforeAndAfterCurrentPage = 7;

        // IUrlHelperFactory fabrica de links o MVC vai-me providenciar um serviço
        public PagingLinksTagHelper(IUrlHelperFactory urlHelperFactory) {
            this.urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        // PageModel vai ser o que vai ser escrito na DIV
        public InfoPaginacao PageModel { get; set; }
        // qual a acção que quero chamar
        public string PageAction { get; set; }

        // para aplicar os meus estilos (na <div>)
        public bool CssClassesEnabled { get; set; } = false;
        public string CssClassPage { get; set; }
        public string CssClassPageNormal { get; set; }
        public string CssClassPageSelected { get; set; }

        // codigo para gerar link: qts pag.s vou pôr e que está em PagingInfo PageModel
        // metodo_override de TagHelper
        public override void Process(TagHelperContext context, TagHelperOutput output) {

            // para gerar os url
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            // criar a tag <div> onde vou colocar pageLinks para cada div
            TagBuilder result = new TagBuilder("div");
            // para balizar o nº max. de links
            int initial = PageModel.PaginaAtual - MaxLinksBeforeAndAfterCurrentPage;
            if (initial < 1) initial = 1;

            int final = PageModel.TotalPages + MaxLinksBeforeAndAfterCurrentPage;
            if (final > PageModel.TotalPages) final = PageModel.TotalPages;

            for (int p = initial; p <= final; p++) {
                // criar a tag <a> hiperligacao
                TagBuilder pageLink = new TagBuilder("a");
                pageLink.Attributes["href"] = urlHelper.Action(PageAction, new { page = p });
                // numeros que vejo: 1 2 3 ...(<a> com nºs)
                pageLink.InnerHtml.Append(p.ToString());
                // se ativas adiciona css a pag.
                if (CssClassesEnabled) {
                    // aplicada a todas as paginas
                    pageLink.AddCssClass(CssClassPage);
                    // se minha pag. == ativa(atual) mostro Css da pagina selecionada senão da normal
                    pageLink.AddCssClass(p == PageModel.PaginaAtual ? CssClassPageSelected : CssClassPageNormal);
                }
                // vou colocar pageLinks para div
                result.InnerHtml.AppendHtml(pageLink);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
