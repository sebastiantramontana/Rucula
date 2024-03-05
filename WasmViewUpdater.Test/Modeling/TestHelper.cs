using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;
using Vitraux.Modeling.Building.Selectors.TableRows;
using Vitraux.Modeling.Models;

namespace Vitraux.Test.Modeling
{
    internal static class TestHelper
    {
        public static ValueModel CreateValueModel(Delegate valueFunc, IEnumerable<TargetElement> targetElements)
            => new ValueModel(valueFunc)
            {
                TargetElements = targetElements.ToArray()
            };

        public static CollectionTableModel CreateCollectionTableModel(Delegate collectionFunc, ElementSelector tableSelector, RowSelector rowSelector, IEnumerable<ValueModel> values, IEnumerable<CollectionTableModel> collectionTables)
            => new(collectionFunc)
            {
                TableSelector = tableSelector,
                RowSelector = rowSelector,
                ModelBuilderData = new ModelBuilderDataFake(values, collectionTables)
            };


        public static ElementTemplateSelector CreateElementTemplateSelectorToId(string templateId, string elementToAppendId, string toChildQuerySelector)
            => new(templateId, new FromTemplateAppendToElementIdSelector(elementToAppendId)) { TargetChildElement = new ElementQuerySelector(toChildQuerySelector) };

        public static ElementTemplateSelector CreateElementTemplateSelectorToQuery(string templateId, string elementToAppendQuerySelector)
            => new(templateId, new FromTemplateAppendToElementQuerySelector(elementToAppendQuerySelector));

        public static TargetElement CreateTargetElement(ElementSelector selector, ElementPlace place)
            => new(selector) { Place = place };

        public static ElementPlace CreateAttributeElementPlace(string attribute)
            => new AttributeElementPlace(attribute);

        public static ElementPlace CreateContentElementPlace()
            => new ContentElementPlace();

        public static void AssertCollectionTableModel(CollectionTableModel actualCollection, CollectionTableModel expectedCollection)
        {
            AssertDelegate(actualCollection.CollectionFunc, expectedCollection.CollectionFunc);
            AssertSelector(actualCollection.TableSelector, expectedCollection.TableSelector);
            AssertRowSelector(actualCollection.RowSelector, expectedCollection.RowSelector);
            AssertModelBuilderData(actualCollection.ModelBuilderData, expectedCollection.ModelBuilderData);
        }

        public static void AssertValueModel(ValueModel actualValueModel, ValueModel expectedValueModel, bool canPlaceBeNull)
        {
            Assert.Multiple(() =>
            {
                AssertDelegate(actualValueModel.ValueFunc, expectedValueModel.ValueFunc);
                Assert.That(actualValueModel.TargetElements.Count(), Is.EqualTo(expectedValueModel.TargetElements.Count()));
            });

            for (int i = 0; i < expectedValueModel.TargetElements.Count(); i++)
            {
                AssertTargetElement(actualValueModel.TargetElements.ElementAt(i), expectedValueModel.TargetElements.ElementAt(i), canPlaceBeNull);
            }
        }

        public static void AssertTargetElement(TargetElement actualTargetElement, TargetElement expectedTargetElement, bool canPlaceBeNull)
        {
            AssertPlace(actualTargetElement.Place, expectedTargetElement.Place, canPlaceBeNull);
            AssertSelector(actualTargetElement.Selector, expectedTargetElement.Selector);
        }

        public static void AssertPlace(ElementPlace actualPlace, ElementPlace expectedPlace, bool canPlaceBeNull)
        {
            if (canPlaceBeNull)
                Assert.That(actualPlace, Is.Null.And.EqualTo(expectedPlace).Or.Not.Null.And.EqualTo(expectedPlace));
            else
                Assert.That(actualPlace, Is.EqualTo(expectedPlace).And.Not.Null);
        }

        public static void AssertSelector(ElementSelector actualSelector, ElementSelector expectedSelector)
        {
            Assert.That(actualSelector, Is.EqualTo(expectedSelector));
        }

        public static void AssertRowSelector(RowSelector actualSelector, RowSelector expectedSelector)
        {
            Assert.That(actualSelector, Is.EqualTo(expectedSelector));
        }

        public static void AssertModelBuilderData(IModelBuilderData actualModelBuilderData, IModelBuilderData expectedModelBuilderData)
        {
            Assert.That(actualModelBuilderData.Values.Count(), Is.EqualTo(expectedModelBuilderData.Values.Count()));
            Assert.That(actualModelBuilderData.CollectionTables.Count(), Is.EqualTo(expectedModelBuilderData.CollectionTables.Count()));

            for (int i = 0; i < actualModelBuilderData.Values.Count(); i++)
            {
                AssertValueModel(actualModelBuilderData.Values.ElementAt(i), expectedModelBuilderData.Values.ElementAt(i), false);
            }

            for (int i = 0; i < actualModelBuilderData.CollectionTables.Count(); i++)
            {
                AssertCollectionTableModel(actualModelBuilderData.CollectionTables.ElementAt(i), expectedModelBuilderData.CollectionTables.ElementAt(i));
            }
        }

        private static void AssertDelegate(Delegate actual, Delegate expected)
        {
            CollectionAssert.AreEqual(GetDelegateIL(expected), GetDelegateIL(actual));
        }

        private static byte[] GetDelegateIL(Delegate d)
            => d.Method.GetMethodBody()?.GetILAsByteArray()!;
    }

    internal class ModelBuilderDataFake(IEnumerable<ValueModel> values, IEnumerable<CollectionTableModel> collectionTables) : IModelBuilderData
    {
        public QueryElementStrategy QueryElementStrategy { get; set; }
        public bool TrackChanges { get; set; }
        IEnumerable<ValueModel> IModelBuilderData.Values { get; } = values;
        IEnumerable<CollectionTableModel> IModelBuilderData.CollectionTables { get; } = collectionTables;
    }
}
