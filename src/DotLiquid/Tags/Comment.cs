using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DotLiquid.Tags
{
	public class Comment : DotLiquid.Block
	{
        private static Regex commentShorthandRegex = new Regex(Liquid.CommentShorthand, RegexOptions.Compiled);

        public static void FromShortHand(StringBuilder @string)
		{
            if (@string.Length > 0)
            {
                Match match = commentShorthandRegex.Match(@string.ToString());

                if (match.Success)
                {
                    @string.Remove(match.Groups[1].Index + match.Groups[1].Length, @string.Length - match.Groups[1].Length - match.Groups[1].Index)
                           .Remove(0, match.Groups[1].Index)
                           .Insert(0, "{% comment %}")
                           .Append("{% endcomment %}");
                }
            }
		}

		public override void Render(Context context, TextWriter result)
		{
		}
	}
}