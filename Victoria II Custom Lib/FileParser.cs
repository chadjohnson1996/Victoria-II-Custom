using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib
{
    public class FileParser
    {
        /// <summary>
        /// parses the key value tree of file specified at the path
        /// </summary>
        /// <param name="path">the path to parse</param>
        /// <returns>the parsed path</returns>
        public async Task<KeyValueNode> Parse(string path)
        {
            using(var sr = new StreamReader(File.OpenRead(path)))
            {
                var data = await sr.ReadToEndAsync();
                var root = new KeyValueNode();
                root.Key = "Root";
                data = RemoveComments(data);
                return ParseHelper(root, data, 0, data.Length);
            }
        }

        private string RemoveComments(string data)
        {
            //TODO: implement
            return data;
        }

        /// <summary>
        /// parse helper method
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private KeyValueNode ParseHelper(KeyValueNode parent, string data, int startIndex, int count)
        {
            var i = startIndex;

            FileParsingState currentNode = null;
            while (i < startIndex + count)
            {
                var currentChar = data[i];
                if (char.IsWhiteSpace(currentChar) && currentNode == null)
                {
                    i++;
                    continue;
                }

                if (currentNode == null)
                {
                    currentNode = new FileParsingState();
                }

                ProcessState(currentNode, currentChar, i);

                if (currentNode.State != FileParsingStateEnum.Finished)
                {
                    i++;
                    continue;
                }

                if (currentNode.Current.IsLeaf)
                {
                    parent.Children.Add(currentNode.Current);
                }

                var toAdd = ParseHelper(currentNode.Current, data, currentNode.ChildrenStartIndex,
                    currentNode.ChildrenIndexCount);
                parent.Children.Add(toAdd);
                i++;
            }

            return parent;
        }

        /// <summary>
        /// processes the state
        /// </summary>
        /// <param name="toProcess">the state to process</param>
        /// <param name="current">the current char to process</param>
        /// <param name="index">the index</param>
        private void ProcessState(FileParsingState toProcess, char current, int index)
        {

        }
    }
}
