using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.Always;
using Vitraux.JsCodeGeneration.QueryElements.OneTimeOnDemand;
using Vitraux.JsCodeGeneration.QueryElements.OneTimeOnInit;
using Vitraux.Modeling;
using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements.Builders;
using Vitraux.Modeling.Building.Selectors.TableRows;
using Vitraux.Test.Example;

namespace Vitraux.Test.JsCodeGeneration
{
    [TestFixture]
    public class JsGeneratorTest
    {
        const string expectedCodeOnInit = """
                                            const element1 = vitraux.elements.document.element1;
                                            const element2 = vitraux.elements.document.element2;
                                            const element3 = vitraux.elements.document.element3;
                                            const element4 = vitraux.elements.document.element4;
                                            """;

        const string expectedCodeOnDemand = """
                                            const element1 = vitraux.getStoredElementById(document, 'document', 'algo-name', 'element1');
                                            const element2 = vitraux.getStoredElementByTemplate('otro-template-id', 'element2');
                                            const element3 = vitraux.getStoredElementsByQuerySelector(document, 'document', '.p-otro > img', 'element3');
                                            const element4 = vitraux.getStoredElementById(document, 'document', 'mascotas-table-id', 'element4');
                                            """;

        const string expectedCodeAlways = """
                                          const element1 = [vitraux.getElementById(document,'algo-name')];
                                          const element2 = [vitraux.getElementByTemplate('otro-template-id')];
                                          const element3 = vitraux.getElementsByQuerySelector(document,'.p-otro > img');
                                          const element4 = [vitraux.getElementById(document,'mascotas-table-id')];
                                          """;

        [Test]
        [TestCase(QueryElementStrategy.OneTimeOnInit, expectedCodeOnInit)]
        [TestCase(QueryElementStrategy.OneTimeOnDemand, expectedCodeOnDemand)]
        [TestCase(QueryElementStrategy.Always, expectedCodeAlways)]
        public void GenerateCodeTest(QueryElementStrategy queryElementStrategy, string expectedCode)
        {
            var onInitDeclaringGenerator = new QueryElementsDeclaringOneTimeOnInitJsCodeGenerator();
            var onInitGenerator = new QueryElementsOneTimeOnInitJsCodeGenerator(onInitDeclaringGenerator);

            var declaringOneTimeOnDemandByIdGenerator = new QueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator();
            var declaringOneTimeOnDemandByQuerySelectorGenerator = new QueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator();
            var declaringOneTimeOnDemandByTemplateGenerator = new QueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator();
            var onDemandGeneratorFactory = new JsQueryElementsOneTimeOnDemandGeneratorFactory(declaringOneTimeOnDemandByIdGenerator, declaringOneTimeOnDemandByQuerySelectorGenerator, declaringOneTimeOnDemandByTemplateGenerator);
            var onDemandGenerator = new QueryElementsOneTimeOnDemandJsCodeGenerator(onDemandGeneratorFactory);

            var declaringAlwaysByIdGenerator = new QueryElementsDeclaringAlwaysByIdJsCodeGenerator();
            var declaringAlwaysByQuerySelectorGenerator = new QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator();
            var declaringAlwaysByTemplateGenerator = new QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator();
            var alwaysGeneratorFactory = new JsQueryElementsAlwaysGeneratorFactory(declaringAlwaysByIdGenerator, declaringAlwaysByQuerySelectorGenerator, declaringAlwaysByTemplateGenerator);
            var onAlwaysGenerator = new QueryElementsAlwaysJsCodeGenerator(alwaysGeneratorFactory);

            var generatorByStrategyFactory = new QueryElementsJsCodeGeneratorByStrategyFactory(onInitGenerator, onDemandGenerator, onAlwaysGenerator);
            var sut = new JsGenerator<Persona>(generatorByStrategyFactory);
            var personaConfig = new PersonaConfiguration() as IModelConfiguration<Persona>;

            var modelBuilder = new ModelBuilder<Persona>() { QueryElementStrategy = queryElementStrategy };
            personaConfig.Configure(modelBuilder, new ElementSelectorBuilder(), new RowSelectorBuilder());

            var code = sut.GenerateJsCode(modelBuilder);

            Assert.That(code, Is.EqualTo(expectedCode));
        }
    }
}
