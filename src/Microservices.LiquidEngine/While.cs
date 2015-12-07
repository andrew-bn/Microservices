using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DotLiquid;
using DotLiquid.Exceptions;
using DotLiquid.Util;

namespace Microservices.LiquidEngine
{
	public class While : DotLiquid.Block
	{
		public override void Render(Context context, TextWriter result)
		{
			while (true)
			{
				try
				{
					RenderAll(NodeList, context, result);
				}
				catch (BreakInterrupt)
				{
					break;
				}
				catch (ContinueInterrupt)
				{
				}
			}
		}
	}
}
