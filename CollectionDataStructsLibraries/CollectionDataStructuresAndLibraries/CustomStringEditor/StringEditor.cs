namespace CustomStringEditor
{
    using System;
    using Wintellect.PowerCollections;

    public class StringEditor
    {
        private BigList<char> rope;

        public StringEditor()
        {
            this.rope = new BigList<char>();
        }

        public string Text
        {
            get { return string.Join("", this.rope); }
        }

        public string Insert(int position, string word)
        {
            if (!this.IsValidIndex(position))
            {
                return "fail";
            }

            this.rope.InsertRange(position, word.ToCharArray());

            return "ok";
        }

        public string Append(string word)
        {
            this.rope.AddRange(word.ToCharArray());

            return "ok";
        }

        public string Delete(int startIndex, int count)
        {
            if (!this.IsValidIndex(startIndex) || !this.IsValidIndex(startIndex + count - 1))
            {
                return "Error";
            }

            this.rope.RemoveRange(startIndex, count);

            return "ok";
        }

        public string Replace(int startIndex, int count, string word)
        {
            if (!this.IsValidIndex(startIndex) || !this.IsValidIndex(startIndex + count - 1))
            {
                return "Error";
            }

            this.Delete(startIndex, count);
            this.Insert(startIndex, word);

            return "ok";
        }

        public void Print()
        {
            Console.WriteLine(this.Text);
        }

        public void End()
        {
            Environment.Exit(0);
        }

        private bool IsValidIndex(int index)
        {
            if (index >= this.rope.Count || index < 0)
            {
                return false;
            }

            return true;
        }
    }
}
