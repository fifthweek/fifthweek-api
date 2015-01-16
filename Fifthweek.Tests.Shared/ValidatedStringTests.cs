namespace Fifthweek.Tests.Shared
{
    using System;

    public abstract class ValidatedStringTests<T> : ValidatedPrimitiveTests<T, string>
    {
        protected virtual bool AppendPadding
        {
            get
            {
                return false;
            }
        }

        public void AssertMinLength(int minLength, bool whitespaceSensitive = true)
        {
            var minString = new string('x', minLength);
            var tooSmallString = minString.Remove(0, 1);

            this.GoodValue(minString);
            this.BadValue(tooSmallString);

            // Test whitespace sensitivity.
            if (whitespaceSensitive)
            {
                this.GoodValue(new string(' ', minLength));
                this.BadValue(new string(' ', minLength - 1));
            }
            else
            {
                // Whitespace should be ignored.
                this.BadValue(tooSmallString + " ");
                this.BadValue(" " + tooSmallString);
                this.BadValue(" " + tooSmallString + " ");
            }
        }

        public void AssertMaxLength(int maxLength, bool whitespaceSensitive = true)
        {
            // Build the max length string from a valid string.
            var padding = new string('x', maxLength - this.ValueA.Length);
            var maxString = this.AppendPadding ? this.ValueA + padding : padding + this.ValueA;
            
            this.GoodValue(maxString);
            this.BadValue("x" + maxString);

            // Test whitespace sensitivity.
            if (whitespaceSensitive)
            {
                this.GoodValue(new string(' ', maxLength));
                this.BadValue(new string(' ', maxLength + 1));     
            }
            else
            {
                // Whitespace should be ignored.
                this.GoodNonExactValue(maxString + " ", maxString);
                this.GoodNonExactValue(" " + maxString, maxString);
                this.GoodNonExactValue(" " + maxString + " ", maxString);
            }
        }

        public void AssertPunctuationAllowed()
        {
            this.AssertCharacter('!', isGood: true);
            this.AssertCharacter('?', isGood: true);
            this.AssertCharacter(',', isGood: true);
            this.AssertCharacter('.', isGood: true);
            this.AssertCharacter(':', isGood: true);
            this.AssertCharacter(';', isGood: true);
            this.AssertCharacter('-', isGood: true);
            this.AssertCharacter('\'', isGood: true);
        }

        public void AssertTabsNotAllowed()
        {
            this.AssertCharacter('\t', isGood: false);
        }

        public void AssertNewLinesNotAllowed()
        {
            this.AssertCharacter('\r', isGood: false);
            this.AssertCharacter('\n', isGood: false);
        }

        public void AssertNewLinesAllowed()
        {
            this.AssertCharacter('\r', isGood: true);
            this.AssertCharacter('\n', isGood: true);
        }

        public void AssertCharacter(char character, bool isGood)
        {
            this.AssertCharacters(new string(new[] { character }), isGood);    
        }
        
        public void AssertCharacters(string characters, bool areGood)
        {
            if (characters.Length >= this.ValueA.Length)
            {
                throw new ArgumentException("Character set must not be larger than the valid 'Value A'", "characters");
            }

            var valueWithRoomForNewLine = this.ValueA.Remove(0, 1);

            var start = characters + valueWithRoomForNewLine;
            var middle = valueWithRoomForNewLine.Insert(valueWithRoomForNewLine.Length / 2, characters);
            var end = valueWithRoomForNewLine + characters;

            if (areGood)
            {
                this.GoodValue(start);
                this.GoodValue(middle);
                this.GoodValue(end);
            }
            else
            {
                this.BadValue(start);
                this.BadValue(middle);
                this.BadValue(end); 
            }
        }
    }
}