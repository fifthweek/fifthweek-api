namespace Fifthweek.Api.Tests.Shared
{
    using System;

    public abstract class ValidatedStringTests<T> : ValidatedPrimitiveTests<T, string>
    {
        public void AssertMinLength(int minLength, bool whitespaceSensitive = true)
        {
            this.GoodValue(new string('x', minLength));
            this.BadValue(new string('x', minLength - 1));

            // Test whitespace sensitivity.
            if (whitespaceSensitive)
            {
                this.GoodValue(new string(' ', minLength));
                this.BadValue(new string(' ', minLength - 1));
            }
        }

        public void AssertMaxLength(int maxLength, bool whitespaceSensitive = true)
        {
            this.GoodValue(new string('x', maxLength));
            this.BadValue(new string('x', maxLength + 1));

            // Test whitespace sensitivity.
            if (whitespaceSensitive)
            {
                this.GoodValue(new string(' ', maxLength));
                this.BadValue(new string(' ', maxLength + 1));     
            }
        }

        public void AssertPunctuationAllowed()
        {
            this.AssertCharacter('!', areGood: true);
            this.AssertCharacter('?', areGood: true);
            this.AssertCharacter(',', areGood: true);
            this.AssertCharacter('.', areGood: true);
            this.AssertCharacter(':', areGood: true);
            this.AssertCharacter(';', areGood: true);
            this.AssertCharacter('-', areGood: true);
            this.AssertCharacter('\'', areGood: true);
        }

        public void AssertTabsNotAllowed()
        {
            this.AssertCharacter('\t', areGood: false);
        }

        public void AssertNewLinesNotAllowed()
        {
            this.AssertCharacter('\r', areGood: false);
            this.AssertCharacter('\n', areGood: false);
        }

        public void AssertCharacter(char character, bool areGood)
        {
            this.AssertCharacters(new string(new[] { character }), areGood);    
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