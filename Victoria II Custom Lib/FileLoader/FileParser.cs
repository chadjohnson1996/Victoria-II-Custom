using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Victoria_II_Custom_Lib
{
    public class FileParser
    {
        /// <summary>
        /// the parsing functions
        /// </summary>
        private Dictionary<FileParsingStateEnum, Action<FileParsingState, char, int>> Parsers { get; }


        public FileParser()
        {
            Parsers = new Dictionary<FileParsingStateEnum, Action<FileParsingState, char, int>>();
            Parsers.Add(FileParsingStateEnum.Initial, ProcessInitialState);
            Parsers.Add(FileParsingStateEnum.ParsingKey, ProcessParsingKey);
            Parsers.Add(FileParsingStateEnum.ParseValue, ProcessParseValue);
            Parsers.Add(FileParsingStateEnum.ParsingEscapedLeafValue, ProcessParsingEscapedLeafValue);
            Parsers.Add(FileParsingStateEnum.ParsingLeafValue, ProcessParsingLeafValue);
            Parsers.Add(FileParsingStateEnum.ParsingNestedValue, ProcessParsingNestedValue);
            Parsers.Add(FileParsingStateEnum.Comment, ProcessComment);
        }
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
                Console.WriteLine(data.Length);
                var root = new KeyValueNode();
                root.Key = "Root";
                root.Children = new Dictionary<string, KeyValueNode>();
                var toReturn = ParseHelper(root, data, 0, data.Length);
                return toReturn;
            }
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

            var currentNode = new FileParsingState();
            while (i < startIndex + count)
            {
                var currentChar = data[i];

                ProcessState(currentNode, currentChar, i);

                if (currentNode.State != FileParsingStateEnum.Finished)
                {
                    i++;
                    continue;
                }

                var nextNode = currentNode.BuildNode();
                if (nextNode.IsLeaf)
                {
                    parent.Children[nextNode.Key] = nextNode;
                }
                else
                {
                    var toAdd = ParseHelper(nextNode, data, currentNode.ChildrenStartIndex,
                    currentNode.ChildrenIndexCount);
                    parent.Children[toAdd.Key] = toAdd;
                }

                currentNode = new FileParsingState();
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
            Parsers[toProcess.State](toProcess, current, index);
        }

        /// <summary>
        /// processes the initial state
        /// </summary>
        /// <param name="toProcess">the state to process</param>
        /// <param name="current">the current char</param>
        /// <param name="index">the index</param>
        private void ProcessInitialState(FileParsingState toProcess, char current, int index)
        {
            if (PrepComment(toProcess, current))
            {
                return;
            }

            if (!char.IsWhiteSpace(current))
            {
                toProcess.KeyBuilder.Append(current);
                toProcess.State = FileParsingStateEnum.ParsingKey;
            }
        }

        /// <summary>
        /// processes parsing the key
        /// </summary>
        /// <param name="toProcess">the state to process</param>
        /// <param name="current">the current char</param>
        /// <param name="index">the index</param>
        private void ProcessParsingKey(FileParsingState toProcess, char current, int index)
        {
            if (current == '=')
            {
                toProcess.State = FileParsingStateEnum.ParseValue;
                return;
            }

            toProcess.KeyBuilder.Append(current);
        }

        /// <summary>
        /// processes the parse value state
        /// </summary>
        /// <param name="toProcess">the state to process</param>
        /// <param name="current"></param>
        /// <param name="index"></param>
        private void ProcessParseValue(FileParsingState toProcess, char current, int index)
        {
            if (PrepComment(toProcess, current))
            {
                return;
            }
            if (char.IsWhiteSpace(current))
            {
                return;
            }

            switch (current)
            {
                case '"':
                    toProcess.State = FileParsingStateEnum.ParsingEscapedLeafValue;
                    break;
                case '{':
                    toProcess.State = FileParsingStateEnum.ParsingNestedValue;
                    toProcess.ChildrenStartIndex = index + 1;
                    toProcess.BracketCount++;
                    break;
                default:
                    toProcess.State = FileParsingStateEnum.ParsingLeafValue;
                    toProcess.ValueBuilder.Append(current);
                    break;
            }
        }

        /// <summary>
        /// processes parsing the escaped leaf value
        /// </summary>
        /// <param name="toProcess">the state to process</param>
        /// <param name="current"></param>
        /// <param name="index"></param>
        private void ProcessParsingEscapedLeafValue(FileParsingState toProcess, char current, int index)
        {
            if (current == '"')
            {
                toProcess.State = FileParsingStateEnum.Finished;
                return;
            }

            toProcess.ValueBuilder.Append(current);
        }

        /// <summary>
        /// processes parsing the leaf value
        /// </summary>
        /// <param name="toProcess">the state to process</param>
        /// <param name="current"></param>
        /// <param name="index"></param>
        private void ProcessParsingLeafValue(FileParsingState toProcess, char current, int index)
        {
            if (char.IsWhiteSpace(current))
            {
                toProcess.State = FileParsingStateEnum.Finished;
                return;
            }

            toProcess.ValueBuilder.Append(current);
        }

        /// <summary>
        /// processes parsing the nested leaf value
        /// </summary>
        /// <param name="toProcess"></param>
        /// <param name="current"></param>
        /// <param name="index"></param>
        private void ProcessParsingNestedValue(FileParsingState toProcess, char current, int index)
        {
            if(PrepComment(toProcess, current))
            {
                return;
            }
            if (current == '{')
            {
                toProcess.BracketCount++;
            }

            if (current == '}')
            {
                toProcess.BracketCount--;
            }

            if (toProcess.BracketCount == 0)
            {
                toProcess.State = FileParsingStateEnum.Finished;
                toProcess.ChildrenIndexCount = index - toProcess.ChildrenStartIndex;
            }
        }

        /// <summary>
        /// preps a comment if applicable
        /// </summary>
        /// <param name="toProcess"></param>
        /// <param name="current"></param>
        /// <returns>true if the character starts a comment and toProcess was successfully preped, false otherwise</returns>
        private bool PrepComment(FileParsingState toProcess, char current)
        {
            if(current != '#')
            {
                return false;
            }

            toProcess.PreviousState = toProcess.State;
            toProcess.State = FileParsingStateEnum.Comment;
            return true;
        }
        /// <summary>
        /// processes a comment
        /// </summary>
        /// <param name="toProcess"></param>
        /// <param name="current"></param>
        /// <param name="index"></param>
        private void ProcessComment(FileParsingState toProcess, char current, int index)
        {
            if(current == '\n' || current == '\r')
            {
                toProcess.State = toProcess.PreviousState;
            }
        }
    }
}
