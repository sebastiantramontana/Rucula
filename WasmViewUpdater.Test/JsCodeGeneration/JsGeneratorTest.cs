using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;
using Vitraux.JsCodeGeneration.Values;
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
                                            const elements0 = globalThis.vitraux.storedElements.elements.document.elements0;
                                            const elements1 = globalThis.vitraux.storedElements.elements.document.elements1;
                                            const elements1_appendTo = globalThis.vitraux.storedElements.elements.document.elements1_appendTo;
                                            const elements2 = globalThis.vitraux.storedElements.elements.document.elements2;
                                            const elements3 = globalThis.vitraux.storedElements.elements.document.elements3;
                                            """;

        const string expectedCodeOnDemand = """
                                            const elements0 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray(document, 'document', 'algo-name', 'elements0');
                                            const elements1 = globalThis.vitraux.storedElements.getStoredElementByTemplateAsArray('otro-template-id', 'elements1');
                                            const elements1_appendTo = globalThis.vitraux.storedElements.getStoredElementByIdAsArray(document, 'document', 'parent-to-add-id', 'elements1_appendTo');
                                            const elements2 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.p-otro > img', 'elements2');
                                            const elements3 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray(document, 'document', 'mascotas-table-id', 'elements3');
                                            """;

        const string expectedCodeAlways = """
                                          const elements0 = globalThis.vitraux.storedElements.getElementByIdAsArray(document, 'algo-name');
                                          const elements1 = globalThis.vitraux.storedElements.getElementByTemplateAsArray('otro-template-id');
                                          const elements1_appendTo = globalThis.vitraux.storedElements.getElementByIdAsArray(document, 'parent-to-add-id');
                                          const elements2 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.p-otro > img');
                                          const elements3 = globalThis.vitraux.storedElements.getElementByIdAsArray(document, 'mascotas-table-id');
                                          """;

        const string expectedExecutedCodeForOnInit = """
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray(document, 'document', 'algo-name', 'elements0');
                                                    globalThis.vitraux.storedElements.getStoredElementByTemplateAsArray('otro-template-id', 'elements1');
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray(document, 'document', 'parent-to-add-id', 'elements1_appendTo');
                                                    globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.p-otro > img', 'elements2');
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray(document, 'document', 'mascotas-table-id', 'elements3');
                                                    """;

        const string expectedCodeForValues = """
                                            if(vm.value0) {
                                                globalThis.vitraux.updating.setElementsAttribute(elements0, 'alt', vm.value0);
                                            }

                                            if(vm.value1) {
                                                globalThis.vitraux.updating.UpdateByTemplate(
                                                elements1[0],
                                                elements1_appendTo,
                                                (templateContent) => globalThis.vitraux.storedElements.getElementByIdAsArray(templateContent, 'child-target-id'),
                                                (targetTemplateChildElements) => globalThis.vitraux.updating.setElementsContent(targetTemplateChildElements, vm.value1));

                                                globalThis.vitraux.updating.setElementsAttribute(elements2, 'data-otro', vm.value1);
                                            }
                                            """;

        [Test]
        [TestCase(QueryElementStrategy.OneTimeOnInit, expectedCodeOnInit)]
        [TestCase(QueryElementStrategy.OneTimeOnDemand, expectedCodeOnDemand)]
        [TestCase(QueryElementStrategy.Always, expectedCodeAlways)]
        public void GenerateCodeTest(QueryElementStrategy queryElementStrategy, string expectedQueryElementsCode)
        {
            var executorMock = new Mock<IJsCodeExecutor>();

            var sut = CreateSut(executorMock.Object);
            var personaConfig = new PersonaConfiguration() as IModelConfiguration<Persona>;

            var modelBuilder = new ModelBuilder<Persona>() { QueryElementStrategy = queryElementStrategy };
            personaConfig.Configure(modelBuilder, new ElementSelectorBuilder(), new RowSelectorBuilder(), new FromTemplateElementSelectorBuilder());

            var actualCode = sut.GenerateJsCode(modelBuilder);

            var expectedCode = expectedQueryElementsCode + Environment.NewLine + Environment.NewLine + expectedCodeForValues;
            Assert.That(actualCode, Is.EqualTo(expectedCode));

            if (queryElementStrategy == QueryElementStrategy.OneTimeOnInit)
                executorMock.Verify(e => e.Excute(expectedExecutedCodeForOnInit), Times.Once);
        }

        [Test]
        public void SampleToTestGeneratedJsCode()
        {
            //Arrange
            var html = """
                <!DOCTYPE html>

                <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                <head>
                    <meta charset="utf-8" />
                    <title>Some Title</title>
                </head>
                <body>
                <div id="test_id">text to change</div>
                </body>
                </html>
                """;

            var options = new ChromeOptions();
            // Avoid opening a Chrome window browser: The test runs in memory
            options.AddArguments("--disable-crash-reporter", "--enable-gpu", "--force-headless-for-tests", "--headless");

            //Also: FireFoxDriver, EdgeDriver and SafariDriver
            IWebDriver driver = new ChromeDriver(options);

            //Load a page with the desired html
            driver.Navigate().GoToUrl("about:blank"); //First blank page...
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"document.write(arguments[0]);", html); //then, write the html

            //Act
            js.ExecuteScript("document.getElementById('test_id').innerHTML = 'text changed'");

            //Assert
            var element = driver.FindElement(By.Id("test_id"));
            Assert.That(element.Text, Is.EqualTo("text changed"));
        }

        private IJsGenerator<Persona> CreateSut(IJsCodeExecutor jsCodeExecutor)
        {
            var getElementByIdAsArrayCall = new GetElementByIdAsArrayCall();
            var getElementsByQuerySelectorCall = new GetElementsByQuerySelectorCall();

            var queryElementsGeneratorByStrategyFactory = CreateQueryElementsJsCodeGeneratorByStrategyFactory(jsCodeExecutor, getElementByIdAsArrayCall, getElementsByQuerySelectorCall);
            var elementNamesGenerator = new ElementNamesGenerator();
            var valueNamesGenerator = new ValueNamesGenerator();
            var valueJsCodeGenerator = CreateValuesJsCodeGenerator(getElementByIdAsArrayCall, getElementsByQuerySelectorCall);

            return new JsGenerator<Persona>(queryElementsGeneratorByStrategyFactory, elementNamesGenerator, valueNamesGenerator, valueJsCodeGenerator);
        }

        private IQueryElementsJsCodeGeneratorByStrategyFactory CreateQueryElementsJsCodeGeneratorByStrategyFactory(IJsCodeExecutor jsCodeExecutor, IGetElementByIdAsArrayCall getElementByIdAsArrayCall, IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall)
        {
            var builder = new QueryElementsJsCodeBuilder();
            var getStoredElementByIdAsArrayCall = new GetStoredElementByIdAsArrayCall();
            var getStoredElementsByQuerySelectorCall = new GetStoredElementsByQuerySelectorCall();
            var queryAppendToElementsDeclaringByTemplateJsCodeGenerator = new QueryAppendToElementsDeclaringByTemplateJsCodeGenerator();
            var queryTemplateCallingJsBuiltInFunctionCodeGenerator = new QueryTemplateCallingJsBuiltInFunctionCodeGenerator(queryAppendToElementsDeclaringByTemplateJsCodeGenerator);

            var onInitGenerator = CreateOnInitGenerator(builder, jsCodeExecutor, getStoredElementByIdAsArrayCall, getStoredElementsByQuerySelectorCall);
            var onDemandGenerator = CreateOnDemandGenerator(builder, getStoredElementByIdAsArrayCall, getStoredElementsByQuerySelectorCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator);
            var onAlwaysGenerator = CreateAlwaysGenerator(builder, getElementByIdAsArrayCall, getElementsByQuerySelectorCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator);

            return new QueryElementsJsCodeGeneratorByStrategyFactory(onInitGenerator, onDemandGenerator, onAlwaysGenerator);
        }

        private IValuesJsCodeGenerator CreateValuesJsCodeGenerator(IGetElementByIdAsArrayCall getElementByIdAsArrayCall, IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall)
        {
            var setElementsAttributeCall = new SetElementsAttributeCall();
            var attributeCodeGenerator = new ElementPlaceAttributeJsCodeGenerator(setElementsAttributeCall);

            var setElementsContentCall = new SetElementsContentCall();
            var contentCodeGenerator = new ElementPlaceContentJsCodeGenerator(setElementsContentCall);

            var codeFormatting = new CodeFormatting();

            var updateByTemplateCall = new UpdateByTemplateCall(codeFormatting);

            var targetElementDirectUpdateJsCodeGeneration = new TargetElementDirectUpdateValueJsCodeGenerator(attributeCodeGenerator, contentCodeGenerator, codeFormatting);
            var targetElementTemplateUpdateJsCodeGeneration = new TargetElementTemplateUpdateValueJsCodeGenerator(updateByTemplateCall, getElementByIdAsArrayCall, getElementsByQuerySelectorCall, setElementsAttributeCall, setElementsContentCall, codeFormatting);
            var targetElementsJsCodeGenerationBuilder = new TargetElementsJsCodeGenerationBuilder(targetElementDirectUpdateJsCodeGeneration, targetElementTemplateUpdateJsCodeGeneration);
            var valueCheckJsCodeGeneration = new ValueCheckJsCodeGeneration();
            var valuesJsCodeGenerationBuilder = new ValuesJsCodeGenerationBuilder(valueCheckJsCodeGeneration, targetElementsJsCodeGenerationBuilder);
            return new ValuesJsCodeGenerator(valuesJsCodeGenerationBuilder);
        }

        private IQueryElementsOneTimeOnInitJsCodeGenerator CreateOnInitGenerator(IQueryElementsJsCodeBuilder builder, IJsCodeExecutor jsCodeExecutor, IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall, IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall)
        {
            var generatorById = new StorageElementJsLineGeneratorById(getStoredElementByIdAsArrayCall);
            var generatorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector(getStoredElementsByQuerySelectorCall);
            var getStoredElementByTemlateAsArrayCall = new GetStoredElementByTemplateAsArrayCall();
            var storageElementJsLineGeneratorById = new StorageElementJsLineGeneratorById(getStoredElementByIdAsArrayCall);
            var storageElementJsLineGeneratorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector(getStoredElementsByQuerySelectorCall);
            var storageFromTemplateElementJsLineGenerator = new StorageFromTemplateElementJsLineGenerator(storageElementJsLineGeneratorById, storageElementJsLineGeneratorByQuerySelector);
            var generatorByTemplate = new StorageElementJsLineGeneratorByTemplate(getStoredElementByTemlateAsArrayCall, storageFromTemplateElementJsLineGenerator);
            var storageElementLineGenerator = new StorageElementJsLineGenerator(generatorById, generatorByQuerySelector, generatorByTemplate);
            var storageElementsBuilder = new StoreElementsJsCodeBuilder(storageElementLineGenerator);
            var initializer = new QueryElementsOneTimeOnInitInitializer(storageElementsBuilder, jsCodeExecutor);
            var onInitDeclaringGenerator = new QueryElementsDeclaringOneTimeOnInitJsCodeGenerator();

            return new QueryElementsOneTimeOnInitJsCodeGenerator(builder, onInitDeclaringGenerator, initializer);
        }

        private static IQueryElementsOneTimeOnDemandJsCodeGenerator CreateOnDemandGenerator(
            IQueryElementsJsCodeBuilder builder,
            IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall,
            IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall,
            IQueryTemplateCallingJsBuiltInFunctionCodeGenerator queryTemplateCallingJsBuiltInFunctionCodeGenerator)
        {
            var declaringOneTimeOnDemandByIdGenerator = new QueryElementsDeclaringOneTimeOnDemandByIdJsCodeGenerator(getStoredElementByIdAsArrayCall);
            var declaringOneTimeOnDemandByQuerySelectorGenerator = new QueryElementsDeclaringOneTimeOnDemandByQuerySelectorJsCodeGenerator(getStoredElementsByQuerySelectorCall);
            var getStoredElementByTemplateAsArrayCall = new GetStoredElementByTemplateAsArrayCall();
            var jsQueryFromTemplateElementsDeclaringOneTimeOnDemandGeneratorFactory = new JsQueryFromTemplateElementsDeclaringOneTimeOnDemandGeneratorFactory(declaringOneTimeOnDemandByIdGenerator, declaringOneTimeOnDemandByQuerySelectorGenerator);
            var declaringOneTimeOnDemandByTemplateGenerator = new QueryElementsDeclaringOneTimeOnDemandByTemplateJsCodeGenerator(getStoredElementByTemplateAsArrayCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator, jsQueryFromTemplateElementsDeclaringOneTimeOnDemandGeneratorFactory);
            var onDemandGeneratorFactory = new JsQueryElementsOneTimeOnDemandGeneratorFactory(declaringOneTimeOnDemandByIdGenerator, declaringOneTimeOnDemandByQuerySelectorGenerator, declaringOneTimeOnDemandByTemplateGenerator);
            var declaringOneTimeOnDemandGenerator = new QueryElementsDeclaringOneTimeOnDemandJsCodeGenerator(onDemandGeneratorFactory);

            return new QueryElementsOneTimeOnDemandJsCodeGenerator(builder, declaringOneTimeOnDemandGenerator);
        }

        private static IQueryElementsAlwaysJsCodeGenerator CreateAlwaysGenerator(
            IQueryElementsJsCodeBuilder builder,
            IGetElementByIdAsArrayCall getElementByIdAsArrayCall,
            IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
            IQueryTemplateCallingJsBuiltInFunctionCodeGenerator queryTemplateCallingJsBuiltInFunctionCodeGenerator)
        {
            var declaringAlwaysByIdGenerator = new QueryElementsDeclaringAlwaysByIdJsCodeGenerator(getElementByIdAsArrayCall);
            var declaringAlwaysByQuerySelectorGenerator = new QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator(getElementsByQuerySelectorCall);
            var getElementByTemplateAsArrayCall = new GetElementByTemplateAsArrayCall();
            var jsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory = new JsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory(declaringAlwaysByIdGenerator, declaringAlwaysByQuerySelectorGenerator);
            var declaringAlwaysByTemplateGenerator = new QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator(getElementByTemplateAsArrayCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator, jsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory);
            var alwaysGeneratorFactory = new JsQueryElementsDeclaringAlwaysGeneratorFactory(declaringAlwaysByIdGenerator, declaringAlwaysByQuerySelectorGenerator, declaringAlwaysByTemplateGenerator);
            var declaringAlwaysGenerator = new QueryElementsDeclaringAlwaysCodeGenerator(alwaysGeneratorFactory);

            return new QueryElementsAlwaysJsCodeGenerator(declaringAlwaysGenerator, builder);
        }
    }
}
