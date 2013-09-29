﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers.CSharp;

namespace Cs2hx
{
	static class WriteTryStatement
	{
		public static void Go(HaxeWriter writer, TryStatementSyntax tryStatement)
		{
			if (tryStatement.Finally != null)
				throw new Exception("Finally blocks are not supported in haxe. " + Utility.Descriptor(tryStatement.Finally));

			writer.WriteLine("try");
			Core.Write(writer, tryStatement.Block);

			foreach (var catchClause in tryStatement.Catches)
			{
				writer.WriteIndent();
				writer.Write("catch (");

				if (catchClause.Declaration == null)
					writer.Write("__ex:Dynamic");
				else
				{
					writer.Write(catchClause.Declaration.Identifier == null ? "__ex" : catchClause.Declaration.Identifier.ValueText);
					writer.Write(":");
					writer.Write(TypeProcessor.ConvertType(catchClause.Declaration.Type));
				}
				writer.Write(")\r\n");
				Core.Write(writer, catchClause.Block);
			}

		}
	}
}
