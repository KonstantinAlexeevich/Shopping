using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Domain.Foundation.Events;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Domain.Foundation.SourceGenerator.Tests
{
  public class EventsInterfaceGeneratorTest
  {
    [Fact]
    public void SimpleGeneratorTest()
    {
      string userSource = @"
using Domain.Foundation;
using Domain.Foundation.Events;

namespace Shopping.Sales.Orders
{
    [AutoApplyInterface]
    public interface IOrderEvent: IEvent
    {
        public string OrderId { get; init; }
        
        public class OrderItemAdded: EventBase, IOrderEvent
        {
            public string OrderId { get; init; }
            public long ProductId { get; init; }
            public uint Count { get; set; }
        }
        
        public class OrderItemRemoved: EventBase, IOrderEvent
        {
            public string OrderId { get; init; }
            public long ProductId { get; init; }
        }
    }
}
";
      var comp = CreateCompilation(userSource);
      var newComp = RunGenerators(comp, out _, new EventsInterfaceGenerator());

      var newFile = newComp.SyntaxTrees
        .Single(x => Path.GetFileName(x.FilePath).EndsWith(".IApplyEvent.cs"));

      Assert.NotNull(newFile);
      var generatedText = newFile.GetText().ToString();
    }



    private static Compilation CreateCompilation(string source)
      => CSharpCompilation.Create("compilation",
        new[] { CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.CSharp9)) },
        new[] { 
            MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location),
            MetadataReference.CreateFromFile(typeof(AutoApplyInterfaceAttribute).GetTypeInfo().Assembly.Location),
            MetadataReference.CreateFromFile(typeof(EventBase).GetTypeInfo().Assembly.Location),
        },
        new CSharpCompilationOptions(OutputKind.ConsoleApplication));

    private static GeneratorDriver CreateDriver(params ISourceGenerator[] generators)
      => CSharpGeneratorDriver.Create(generators);

    private static Compilation RunGenerators(Compilation compilation, out ImmutableArray<Diagnostic> diagnostics, params ISourceGenerator[] generators)
    {
      CreateDriver(generators).RunGeneratorsAndUpdateCompilation(compilation, out var newCompilation, out diagnostics);
      return newCompilation;
    }
  }
}