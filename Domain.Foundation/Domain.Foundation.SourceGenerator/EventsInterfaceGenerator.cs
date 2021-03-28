using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Domain.Foundation.SourceGenerator
{
    [Generator]
    public class EventsInterfaceGenerator: ISourceGenerator
    {
        private string _autoApplyInterfaceAttribute = "Domain.Foundation.AutoApplyInterfaceAttribute";
        private string _event = "Domain.Foundation.Events.IEvent";
        
        public void Initialize(GeneratorInitializationContext context)
        { }

        public void Execute(GeneratorExecutionContext context)
        {
            var compilation = context.Compilation;

            var autoAttributeSymbol = compilation.GetTypeByMetadataName(_autoApplyInterfaceAttribute) ?? throw null;
            var iEventSymbol = compilation.GetTypeByMetadataName(_event) ?? throw null;

            var attributedInterfaces = compilation.SemanticTrees()
                .SelectMany(x => x.SyntaxTree
                    .GetRoot()
                    .DescendantNodesAndSelf()
                    .OfType<InterfaceDeclarationSyntax>()
                    .Select(y => x.SemanticModel.GetDeclaredSymbol(y))
                    .Where(y => y.ImplementInterface(iEventSymbol))
                    .Where(y => y.GetAttributes().Any(z => autoAttributeSymbol.Equals(z.AttributeClass)))
                );

            var subTypes = compilation.SemanticTrees()
                .SelectMany(x => x.SyntaxTree
                    .GetRoot()
                    .DescendantNodesAndSelf()
                    .OfType<ClassDeclarationSyntax>()
                    .Select(y => x.SemanticModel.GetDeclaredSymbol(y))
                    .Where(y => attributedInterfaces.Any(y.ImplementInterface))
                );
            
            foreach (var symbol in attributedInterfaces)
            {
                var generatedInterface = symbol.GetIApplyInterfaceName();
                
                var eventsInterfacesDeclaration = subTypes
                    .Where(x => x.ImplementInterface(symbol))
                    .GetIApplyInterfacesDeclaration();

                var generatedSource = GetIApplyEventInterface(
                    symbol.ContainingNamespace.ToString(),
                    generatedInterface,
                    eventsInterfacesDeclaration
                );
                
                context.AddSource($"{generatedInterface}.cs", generatedSource);
            }
        }
        
        string GetIApplyEventInterface(string @namespace, string @class, string inherits) => 
            @$"using Domain.Foundation.Tactical;
namespace {@namespace} 
{{
    public interface {@class}: 
        {inherits}
    {{ }}
}}
";
    }
}