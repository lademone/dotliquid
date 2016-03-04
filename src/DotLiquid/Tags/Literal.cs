using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DotLiquid.Exceptions;
using DotLiquid.Util;

namespace DotLiquid.Tags
{
	/// <summary>
	/// Literal
	/// Literal outputs text as is, usefull if your template contains Liquid syntax.
	/// 
	/// {% literal %}{% if user = 'tobi' %}hi{% endif %}{% endliteral %}
	/// 
	/// or (shorthand version)
	/// 
	/// {{{ {% if user = 'tobi' %}hi{% endif %} }}}
	/// </summary>
	public class Literal : DotLiquid.Block
	{
        private static Regex literalShorthandRegex = new Regex(Liquid.LiteralShorthand, RegexOptions.Compiled);

        public static void FromShortHand(StringBuilder @string)
		{
			if (@string.Length > 0)
            {
                Match match = literalShorthandRegex.Match(@string.ToString());

                if (match.Success)
                {
                    @string.Remove(match.Groups[1].Index + match.Groups[1].Length, @string.Length - match.Groups[1].Length - match.Groups[1].Index)
                           .Remove(0, match.Groups[1].Index)
                           .Insert(0, "{% literal %}")
                           .Append("{% endliteral %}");
                }
            }
		}

		protected override void Parse(List<string> tokens)
		{
			NodeList = NodeList ?? new List<object>();
			NodeList.Clear();

			string token;
			while ((token = tokens.Shift()) != null)
			{
				Match fullTokenMatch = FullToken.Match(token);
				if (fullTokenMatch.Success && BlockDelimiter == fullTokenMatch.Groups[1].Value)
				{
					EndTag();
					return;
				}
				else
					NodeList.Add(token);
			}

			AssertMissingDelimitation();
		}
	}
}