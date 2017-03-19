using System;
using System.IO;
using NUnit.Framework;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Module.CorruptTransportMessage.Tests
{
	[TestFixture]
	public class CorruptTransportMessageSectionFixture
	{
		[Test]
		[TestCase("CorruptTransportMessage.config")]
		[TestCase("CorruptTransportMessage-Grouped.config")]
		public void Should_be_able_to_load_the_configuration(string file)
		{
			var section = ConfigurationSectionProvider.OpenFile<CorruptTransportMessageSection>("shuttle", "corruptTransportMessage",
				Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"config-files\{0}", file)));

			Assert.IsNotNull(section);
			Assert.AreEqual(".\\folder", section.Folder);
		}
	}
}