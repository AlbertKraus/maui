﻿using Maui.Controls.Sample;
using Microsoft.Maui.Appium;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using NUnit.Framework;
using TestUtils.Appium.UITests;
using Xamarin.UITest;

namespace Microsoft.Maui.AppiumTests
{
	public class DragAndDropUITests : UITestBase
	{
		const string DragAndDropGallery = "* marked:'Drag and Drop Gallery'";
		public DragAndDropUITests(TestDevice device)
			: base(device)
		{
		}

		protected override void FixtureSetup()
		{
			base.FixtureSetup();
			App.NavigateToGallery(DragAndDropGallery);
		}

		protected override void FixtureTeardown()
		{
			base.FixtureTeardown();
			App.NavigateBack();
		}

		[Test]
		public void DragEvents()
		{
			App.WaitForElement("TargetView");
			App.EnterText("TargetView", "DragAndDropEvents");
			App.Tap("GoButton");

			App.WaitForElement("LabelDragElement");
			App.DragAndDrop("LabelDragElement", "DragTarget");
			App.WaitForElement("DragEventsLabel");

			var textAfterDrag = App.Query("DragEventsLabel").First().Text;
			Assert.True(textAfterDrag.Contains("DragStarting", StringComparison.OrdinalIgnoreCase));
			Assert.True(textAfterDrag.Contains("DragOver", StringComparison.OrdinalIgnoreCase));
			Assert.True(textAfterDrag.Contains("DropCompleted", StringComparison.OrdinalIgnoreCase));
		}

		[Test]
		public void DragAndDropBetweenLayouts()
		{
			App.WaitForElement("TargetView");
			App.EnterText("TargetView", "DragAndDropBetweenLayouts");
			App.Tap("GoButton");

			App.Tap("ResetButton");

			App.WaitForElement("Red");
			App.WaitForElement("Green");
			App.DragAndDrop("Red", "Green");
			App.WaitForElement("DragEventsLabel");

			var textAfterDrag = App.Query("DragEventsLabel").First().Text;
			Assert.True(textAfterDrag.Contains("DragStarting", StringComparison.OrdinalIgnoreCase));
			Assert.True(textAfterDrag.Contains("DragOver", StringComparison.OrdinalIgnoreCase));
			Assert.True(textAfterDrag.Contains("DropCompleted", StringComparison.OrdinalIgnoreCase));
			Assert.True(textAfterDrag.Contains("RainbowColorsAdd:Red", StringComparison.OrdinalIgnoreCase));
		}

		[Test]
		public void PlatformDragEventArgs()
		{
			App.WaitForElement("TargetView");
			App.EnterText("TargetView", "DragAndDropEventArgs");
			App.Tap("GoButton");

			App.WaitForElement("LabelDragElement");
			App.DragAndDrop("LabelDragElement", "DragTarget");
			App.WaitForElement("DragEventsLabel");

			var textAfterDrag = App.Query("DragEventsLabel").First().Text;

			if (UITestContext?.TestConfig.TestDevice == TestDevice.iOS ||
				UITestContext?.TestConfig.TestDevice == TestDevice.Mac)
			{
				Assert.True(textAfterDrag.Contains("DragStarting:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragStarting:DragInteraction", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragStarting:DragSession", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("DropCompleted:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DropCompleted:DropInteraction", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DropCompleted:DropSession", StringComparison.OrdinalIgnoreCase));

				// Until the UI test can drag over an item without placing it down
				//Assert.True(textAfterDrag.Contains("DragLeave:Sender", StringComparison.OrdinalIgnoreCase));
				//Assert.True(textAfterDrag.Contains("DragLeave:DropInteraction", StringComparison.OrdinalIgnoreCase));
				//Assert.True(textAfterDrag.Contains("DragLeave:DropSession", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("DragOver:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragOver:DropInteraction", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragOver:DropSession", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("Drop:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("Drop:DropInteraction", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("Drop:DropSession", StringComparison.OrdinalIgnoreCase));
			}
			else if (UITestContext?.TestConfig.TestDevice == TestDevice.Android)
			{
				Assert.True(textAfterDrag.Contains("DragStarting:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragStarting:MotionEvent", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("DropCompleted:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DropCompleted:DragEvent", StringComparison.OrdinalIgnoreCase));

				// Until the UI test can drag over an item without placing it down
				//Assert.True(textAfterDrag.Contains("DragLeave:Sender", StringComparison.OrdinalIgnoreCase));
				//Assert.True(textAfterDrag.Contains("DragLeave:DragEvent", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("DragOver:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragOver:DragEvent", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("Drop:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("Drop:DragEvent", StringComparison.OrdinalIgnoreCase));

			}
			else if (UITestContext?.TestConfig.TestDevice == TestDevice.Windows)
			{
				Assert.True(textAfterDrag.Contains("DragStarting:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragStarting:DragStartingEventArgs", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragStarting:Handled", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("DropCompleted:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DropCompleted:DropCompletedEventArgs", StringComparison.OrdinalIgnoreCase));

				// Until the UI test can drag over an item without placing it down
				//Assert.True(textAfterDrag.Contains("DragLeave:Sender", StringComparison.OrdinalIgnoreCase));
				//Assert.True(textAfterDrag.Contains("DragLeave:DragEventArgs", StringComparison.OrdinalIgnoreCase));
				//Assert.True(textAfterDrag.Contains("DragLeave:Handled", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("DragOver:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("DragOver:DragEventArgs", StringComparison.OrdinalIgnoreCase));

				Assert.True(textAfterDrag.Contains("Drop:Sender", StringComparison.OrdinalIgnoreCase));
				Assert.True(textAfterDrag.Contains("Drop:DragEventArgs", StringComparison.OrdinalIgnoreCase));
			}
		}

		[Test]
		public void DragStartEventCoordinates()
		{
			App.WaitForElement("TargetView");
			App.EnterText("TargetView", "DragAndDropBetweenLayouts");
			App.Tap("GoButton");

			App.Tap("ResetButton");

			App.WaitForElement("Blue");
			App.WaitForElement("Green");
			App.DragAndDrop("Blue", "Green");

			var dragStartRelativeToSelf = GetCoordinatesFromLabel(App.Query("DragStartRelativeSelf").First().Text);
			var dragStartRelativeToScreen = GetCoordinatesFromLabel(App.Query("DragStartRelativeScreen").First().Text);
			var dragStartRelativeToLabel = GetCoordinatesFromLabel(App.Query("DragStartRelativeLabel").First().Text);

			Assert.NotNull(dragStartRelativeToSelf);
			Assert.NotNull(dragStartRelativeToScreen);
			Assert.NotNull(dragStartRelativeToLabel);

			Assert.True(dragStartRelativeToSelf!.Value.X > 0 && dragStartRelativeToSelf!.Value.Y > 0);
			Assert.True(dragStartRelativeToScreen!.Value.X > 0 && dragStartRelativeToScreen!.Value.Y > 0);

			// The position of the drag relative to itself should be less than that relative to the screen
			// There are other elements in the screen, plus the ContentView of the test has some margin
			Assert.True(dragStartRelativeToSelf!.Value.X < dragStartRelativeToScreen!.Value.X);
			Assert.True(dragStartRelativeToSelf!.Value.Y < dragStartRelativeToScreen!.Value.Y);

			// Since the label is below the the box, the Y position of the drag relative to the label should be negative
			Assert.True(dragStartRelativeToLabel!.Value.Y < 0);
		}

		[Test]
		public void DragEventCoordinates()
		{
			App.WaitForElement("TargetView");
			App.EnterText("TargetView", "DragAndDropBetweenLayouts");
			App.Tap("GoButton");

			App.Tap("ResetButton");

			App.WaitForElement("Blue");
			App.WaitForElement("Green");
			App.DragAndDrop("Blue", "Green");

			var dragRelativeToDrop = GetCoordinatesFromLabel(App.Query("DragRelativeDrop").First().Text);
			var dragRelativeToScreen = GetCoordinatesFromLabel(App.Query("DragRelativeScreen").First().Text);
			var dragRelativeToLabel = GetCoordinatesFromLabel(App.Query("DragRelativeLabel").First().Text);
			var dragStartRelativeToScreen = GetCoordinatesFromLabel(App.Query("DragStartRelativeScreen").First().Text);

			Assert.NotNull(dragRelativeToDrop);
			Assert.NotNull(dragRelativeToScreen);
			Assert.NotNull(dragRelativeToLabel);
			Assert.NotNull(dragStartRelativeToScreen);

			Assert.True(dragRelativeToDrop!.Value.X > 0 && dragRelativeToDrop!.Value.Y > 0);
			Assert.True(dragRelativeToScreen!.Value.X > 0 && dragRelativeToScreen!.Value.Y > 0);

			// The position of the drag relative to the drop location should be less than that relative to the screen
			// There are other elements in the screen, plus the ContentView of the test has some margin
			Assert.True(dragRelativeToDrop!.Value.X < dragRelativeToScreen!.Value.X);
			Assert.True(dragRelativeToDrop!.Value.Y < dragRelativeToScreen!.Value.Y);

			// Since the label is below the the box, the Y position of the drag relative to the label should be negative
			Assert.True(dragRelativeToLabel!.Value.Y < 0);

			// The drag is executed left to right, so the X value should be higher than where it started
			Assert.True(dragRelativeToScreen!.Value.X > dragStartRelativeToScreen!.Value.X);
		}

		[Test]
		public void DropEventCoordinates()
		{
			App.WaitForElement("TargetView");
			App.EnterText("TargetView", "DragAndDropBetweenLayouts");
			App.Tap("GoButton");

			App.Tap("ResetButton");

			App.WaitForElement("Blue");
			App.WaitForElement("Green");
			App.DragAndDrop("Blue", "Green");

			var dropRelativeToLayout = GetCoordinatesFromLabel(App.Query("DropRelativeLayout").First().Text);
			var dropRelativeToScreen = GetCoordinatesFromLabel(App.Query("DropRelativeScreen").First().Text);
			var dropRelativeToLabel = GetCoordinatesFromLabel(App.Query("DropRelativeLabel").First().Text);

			var dragRelativeToLabel = GetCoordinatesFromLabel(App.Query("DragRelativeLabel").First().Text);
			var dragStartRelativeToScreen = GetCoordinatesFromLabel(App.Query("DragStartRelativeScreen").First().Text);

			Assert.NotNull(dropRelativeToLayout);
			Assert.NotNull(dropRelativeToScreen);
			Assert.NotNull(dropRelativeToLabel);

			Assert.NotNull(dragRelativeToLabel);
			Assert.NotNull(dragStartRelativeToScreen);

			Assert.True(dropRelativeToLayout!.Value.X > 0 && dropRelativeToLayout!.Value.Y > 0);
			Assert.True(dropRelativeToScreen!.Value.X > 0 && dropRelativeToScreen!.Value.Y > 0);

			// The position of the drop relative the layout should be less than that relative to the screen
			// There are other elements in the screen, plus the ContentView of the test has some margin
			Assert.True(dropRelativeToLayout!.Value.X < dropRelativeToScreen!.Value.X);
			Assert.True(dropRelativeToLayout!.Value.Y < dropRelativeToScreen!.Value.Y);

			// Since the label is below the the box, the Y position of the drop relative to the label should be negative
			Assert.True(dropRelativeToLabel!.Value.Y < 0);

			// The drop is executed left to right, so the X value should be higher than where it started
			Assert.True(dropRelativeToScreen!.Value.X > dragStartRelativeToScreen!.Value.X);

			// The label receiving the coordinates of the drop is below that which receives the coordinates of the drag
			// Therefore, the label that receives the coordinates of the drop should have a smaller Y value (more negative)
			Assert.True(dropRelativeToLabel!.Value.Y < dragRelativeToLabel!.Value.Y);
		}

		// Helper function to parse out the X and Y coordinates from text labels 'Drag position: (x),(y)'
		Point? GetCoordinatesFromLabel(string? labelText)
		{
			if (labelText is null)
				return null;

			var i = labelText.IndexOf(':', StringComparison.Ordinal);

			if (i == -1)
				return null;

			var coordinates = labelText[(i + 1)..].Split(",");
			var x = int.Parse(coordinates[0]);
			var y = int.Parse(coordinates[1]);

			return new Point(x, y);
		}
	}
}