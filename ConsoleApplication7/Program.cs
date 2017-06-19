
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            var code = new DynamicCode();
            code.GenerateCode();
           
        }
    }

    public class DynamicCode
    {
        public void GenerateCode()
        {
            var unit = new CodeCompileUnit();

            var samplenamespace = new CodeNamespace("XiZhang.com");
            samplenamespace.Imports.Add(new CodeNamespaceImport("System"));

            var customerclass = new CodeTypeDeclaration("Customer");
            customerclass.IsClass = true;
            customerclass.TypeAttributes = TypeAttributes.Public;

            samplenamespace.Types.Add(customerclass);

            unit.Namespaces.Add(samplenamespace);

            var outputFile = "Customer.cs";

            var field = new CodeMemberField(typeof(System.String), "_Id");
            field.Attributes = MemberAttributes.Private;
            customerclass.Members.Add(field);

            var property = new CodeMemberProperty();
            property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            property.Name = "Id";
            property.HasGet = true;
            property.HasSet= true;
            property.Type = new CodeTypeReference(typeof(System.String));
            //create comments..
            property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_Id")));
            property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_Id"),new CodePropertySetValueReferenceExpression()));
            //create method
            //create constructor
            //create event
            customerclass.Members.Add(property);

            var provider = CodeDomProvider.CreateProvider("CSharp");

            var options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            options.BlankLinesBetweenMembers = true;
            using (var w = new StreamWriter(outputFile))
            {
                provider.GenerateCodeFromCompileUnit(unit, w, options);
            }

            //create dll
            var compilerParameters = new CompilerParameters();
            compilerParameters.GenerateExecutable = false;
            compilerParameters.GenerateInMemory = false;
            compilerParameters.OutputAssembly = "customer.dll";
            var result = provider.CompileAssemblyFromDom(compilerParameters, unit);
        }
    }
}
