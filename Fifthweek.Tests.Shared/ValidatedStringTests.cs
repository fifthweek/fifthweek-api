namespace Fifthweek.Tests.Shared
{
    using System;

    public abstract class ValidatedStringTests<T> : ValidatedPrimitiveTests<T, string>
    {
        protected virtual char AppendCharacter
        {
            get
            {
                return 'x';
            }
        }

        protected virtual bool AppendPadding
        {
            get
            {
                return false;
            }
        }

        public void AssertMinLength(int minLength, bool whitespaceSensitive = true)
        {
            var minString = new string(this.AppendCharacter, minLength);
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
            var padding = new string(this.AppendCharacter, maxLength - this.ValueA.Length);
            var maxString = this.AppendPadding ? this.ValueA + padding : padding + this.ValueA;
            
            this.GoodValue(maxString);
            this.BadValue(this.AppendCharacter + maxString);

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
            this.AssertPunctuation(true);
        }

        public void AssertPunctuationNotAllowed()
        {
            this.AssertPunctuation(false);
        }

        public void AssertTabsNotAllowed(bool whitespaceSensitive = true)
        {
            this.AssertCharacter('\t', isGood: false, whitespaceSensitive: whitespaceSensitive);
        }

        public void AssertNewLinesNotAllowed(bool whitespaceSensitive = true)
        {
            this.AssertCharacter('\r', isGood: false, whitespaceSensitive: whitespaceSensitive);
            this.AssertCharacter('\n', isGood: false, whitespaceSensitive: whitespaceSensitive);
        }

        public void AssertNewLinesAllowed()
        {
            this.AssertCharacter('\r', isGood: true);
            this.AssertCharacter('\n', isGood: true);
        }

        public void AssertCharacter(char character, bool isGood, bool whitespaceSensitive = true)
        {
            this.AssertCharacters(new string(new[] { character }), isGood, whitespaceSensitive);    
        }

        public void AssertCharacters(string characters, bool areGood, bool whitespaceSensitive = true)
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
                if (whitespaceSensitive)
                {
                    this.GoodValue(start);
                    this.GoodValue(end);
                }

                this.GoodValue(middle);
            }
            else
            {
                if (whitespaceSensitive)
                {
                    this.BadValue(start);
                    this.BadValue(end);
                }
                
                this.BadValue(middle);
            }
        }

        private void AssertPunctuation(bool isGood)
        {
            this.AssertCharacter('!', isGood: isGood);
            this.AssertCharacter('?', isGood: isGood);
            this.AssertCharacter(',', isGood: isGood);
            this.AssertCharacter('.', isGood: isGood);
            this.AssertCharacter(':', isGood: isGood);
            this.AssertCharacter(';', isGood: isGood);
            this.AssertCharacter('-', isGood: isGood);
            this.AssertCharacter('\'', isGood: isGood);
        }
    }
}