using Moq;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;
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
                                            const element0 = globalThis.vitraux.storedElements.elements.document.element0;
                                            const element1 = globalThis.vitraux.storedElements.elements.document.element1;
                                            const element2 = globalThis.vitraux.storedElements.elements.document.element2;
                                            const element3 = globalThis.vitraux.storedElements.elements.document.element3;
                                            """;

        const string expectedCodeOnDemand = """
                                            const element0 = globalThis.vitraux.storedElements.getStoredElementById(document, 'document', 'algo-name', 'element0');
                                            const element1 = globalThis.vitraux.storedElements.getStoredElementByTemplate('otro-template-id', 'element1');
                                            const element2 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.p-otro > img', 'element2');
                                            const element3 = globalThis.vitraux.storedElements.getStoredElementById(document, 'document', 'mascotas-table-id', 'element3');
                                            """;

        const string expectedCodeAlways = """
                                          const element0 = [globalThis.vitraux.storedElements.getElementById(document,'algo-name')];
                                          const element1 = [globalThis.vitraux.storedElements.getElementByTemplate('otro-template-id')];
                                          const element2 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document,'.p-otro > img');
                                          const element3 = [globalThis.vitraux.storedElements.getElementById(document,'mascotas-table-id')];
                                          """;

        const string expectedExecutedCodeForOnInit = """
                                                    globalThis.vitraux.storedElements.getStoredElementById(document, 'document', 'algo-name', 'element0');
                                                    globalThis.vitraux.storedElements.getStoredElementByTemplate('otro-template-id', 'element1');
                                                    globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.p-otro > img', 'element2');
                                                    globalThis.vitraux.storedElements.getStoredElementById(document, 'document', 'mascotas-table-id', 'element3');
                                                    """;

        [Test]
        [TestCase(QueryElementStrategy.OneTimeOnInit, expectedCodeOnInit)]
        [TestCase(QueryElementStrategy.OneTimeOnDemand, expectedCodeOnDemand)]
        [TestCase(QueryElementStrategy.Always, expectedCodeAlways)]
        public void GenerateCodeTest(QueryElementStrategy queryElementStrategy, string expectedCode)
        {
            var executorMock = new Mock<IJsCodeExecutor>();
            var builder = new QueryElementsJsCodeBuilder();
            var elementNamesGenerator = new ElementNamesGenerator();
            var onInitGenerator = CreateOnInitGenerator(builder, elementNamesGenerator, executorMock.Object);
            var onDemandGenerator = CreateOnDemandGenerator(builder, elementNamesGenerator);
            var onAlwaysGenerator = CreateAlwaysGenerator(builder, elementNamesGenerator);

            var generatorByStrategyFactory = new QueryElementsJsCodeGeneratorByStrategyFactory(onInitGenerator, onDemandGenerator, onAlwaysGenerator);
            var sut = new JsGenerator<Persona>(generatorByStrategyFactory);
            var personaConfig = new PersonaConfiguration() as IModelConfiguration<Persona>;

            var modelBuilder = new ModelBuilder<Persona>() { QueryElementStrategy = queryElementStrategy };
            personaConfig.Configure(modelBuilder, new ElementSelectorBuilder(), new RowSelectorBuilder());

            var code = sut.GenerateJsCode(modelBuilder);

            Assert.That(code, Is.EqualTo(expectedCode));

            if (queryElementStrategy == QueryElementStrategy.OneTimeOnInit)
                executorMock.Verify(e => e.Excute(expectedExecutedCodeForOnInit), Times.Once);
        }

        private IQueryElementsOneTimeOnInitJsCodeGenerator CreateOnInitGenerator(IQueryElementsJsCodeBuilder builder, IElementNamesGenerator elementNamesGenerator, IJsCodeExecutor jsCodeExecutor)
        {
            var generatorById = new StorageElementJsLineGeneratorById();
            var generatorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector();
            var generatorByTemplate = new StorageElementJsLineGeneratorByTemplate();
            var storageElementLineGenerator = new StorageElementJsLineGenerator(generatorById, generatorByQuerySelector, generatorByTemplate);
            var storageElementsBuilder = new StoreElementsJsCodeBuilder(storageElementLineGenerator);
            var initializer = new QueryElementsOneTimeOnInitInitializer(storageElementsBuilder, jsCodeExecutor);
            var onInitDeclaringGenerator = new QueryElementsDeclaringOneTimeOnInitJsCodeGenerator();

            return new QueryElementsOneTimeOnInitJsCodeGenerator(builder, onInitDeclaringGenerator, elementNamesGenerator, initializer);
        }

        private static IQueryElementsOneTimeOnDemandJsCodeGenerator CreateOnDemandGenerator(IQueryElementsJsCodeBuilder builder, IElementNamesGenerator elementNamesGenerator)
        {
            var declaringOneTimeOnDemandByIdGenerator = new QueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator();
            var declaringOneTimeOnDemandByQuerySelectorGenerator = new QueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator();
            var declaringOneTimeOnDemandByTemplateGenerator = new QueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator();
            var onDemandGeneratorFactory = new JsQueryElementsOneTimeOnDemandGeneratorFactory(declaringOneTimeOnDemandByIdGenerator, declaringOneTimeOnDemandByQuerySelectorGenerator, declaringOneTimeOnDemandByTemplateGenerator);
            var declaringOneTimeOnDemandGenerator = new QueryElementsDeclaringOneTimeOnDemandJsCodeGenerator(onDemandGeneratorFactory);
            return new QueryElementsOneTimeOnDemandJsCodeGenerator(builder, declaringOneTimeOnDemandGenerator, elementNamesGenerator);
        }

        private static IQueryElementsAlwaysJsCodeGenerator CreateAlwaysGenerator(IQueryElementsJsCodeBuilder builder, IElementNamesGenerator elementNamesGenerator)
        {
            var declaringAlwaysByIdGenerator = new QueryElementsDeclaringAlwaysByIdJsCodeGenerator();
            var declaringAlwaysByQuerySelectorGenerator = new QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator();
            var declaringAlwaysByTemplateGenerator = new QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator();
            var alwaysGeneratorFactory = new JsQueryElementsDeclaringAlwaysGeneratorFactory(declaringAlwaysByIdGenerator, declaringAlwaysByQuerySelectorGenerator, declaringAlwaysByTemplateGenerator);
            var declaringAlwaysGenerator = new QueryElementsDeclaringAlwaysCodeGenerator(alwaysGeneratorFactory);
            return new QueryElementsAlwaysJsCodeGenerator(declaringAlwaysGenerator, builder, elementNamesGenerator);
        }
    }
}
