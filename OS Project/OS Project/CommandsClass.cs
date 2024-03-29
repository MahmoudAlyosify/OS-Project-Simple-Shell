﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OS_Project
{
    public class Commands
    {
        public string[] CommandArg;
        public Commands(string command)
        {
            CommandArg = command.Split(" ");

            if (CommandArg.Length == 1)
            {

                Command(CommandArg);
            }
            else if (CommandArg.Length > 1)
            {
                Command2Arg(CommandArg);
            }

        }
        static void Command(string[] CommandArray)
        {
            if (CommandArray[0].ToLower() == "quit")
            {
                Environment.Exit(0);
            }

            else if (CommandArray[0].ToLower() == "cls")
            {
                Console.Clear();
            }

            else if (CommandArray[0].ToLower() == "help")
            {
                Console.WriteLine("cd   Change the current default directory to. If the argument is not present, report the current directory." +
                    " If the directory does not exist an appropriate error should be reported.");
                Console.WriteLine("cls   Clear the screen.");
                Console.WriteLine("dir   List the contents of directory.");
                Console.WriteLine("help   Display the user manual using the more filter.");
                Console.WriteLine("quit   Quit the shell.");
                Console.WriteLine("copy   Copies one or more files to another location.");
                Console.WriteLine("del   Deletes one or more files.");
                Console.WriteLine("md   Creates a directory.");
                Console.WriteLine("rd   Removes a directory.");
                Console.WriteLine("rename   Renames a file.");
                Console.WriteLine("type   Displays the contents of a text file.");
                Console.WriteLine("import   import text file(s) from your computer.");
                Console.WriteLine("export   export text file(s) to your computer.");
            }

            else if (CommandArray[0].ToLower() == "dir")//dir for directory without path(subdirectory)
            {
                int FileCounter = 0;
                int FolderCounter = 0;
                int FileSizes = 0;
                for (int i = 0; i < Program.CurrentDirectory.DirectoryTable.Count; i++)
                {
                    if (Program.CurrentDirectory.DirectoryTable[i].fileAttribute == 1)  //ده لو هو فولدر
                    {
                        FolderCounter++;
                        string s = new string(Program.CurrentDirectory.DirectoryTable[i].FileName);
                        Console.WriteLine(" <DIR>   " + s);
                    }
                    else if (Program.CurrentDirectory.DirectoryTable[i].fileAttribute == 0)
                    {
                        FileCounter++;
                        FileSizes += Program.CurrentDirectory.DirectoryTable[i].fileSize;
                        string m = "";
                        m += new string(Program.CurrentDirectory.DirectoryTable[i].FileName);
                        Console.Write(" <File> ");
                        Console.Write(" " + m);
                        Console.WriteLine();
                    }
                }

                Console.Write(FileCounter + " File(s)    ");
                if (FileCounter > 0)
                {
                    Console.Write(FileSizes);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                }
                Console.WriteLine(FolderCounter + " Dir(s)     " + FatTable.FreeSpaces() + (" bytes free"));
            }

            else
            {
                Console.WriteLine(CommandArray[0] + " is not a valid command.");
                Console.WriteLine("please valid Command ");
            }
        }
        static void Command2Arg(string[] CommandArray2Arg)
        {
            if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "cd")
            {
                Console.WriteLine("cd   Change the current default directory to. If the argument is not present, report the current directory. If the directory does not exist an appropriate error should be reported.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "cls")
            {
                Console.WriteLine("cls   Clear the screen.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "dir")
            {
                Console.WriteLine("dir   List the contents of directory.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "help")
            {
                Console.WriteLine("help   Display the user manual using the more filter.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "quit")
            {
                Console.WriteLine("quit   Quit the shell.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "copy")
            {
                Console.WriteLine("copy   Copies one or more files to another location.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "del")
            {
                Console.WriteLine("del   Deletes one or more files.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "md")
            {
                Console.WriteLine("md   Creates a directory.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "rd")
            {
                Console.WriteLine("rd   Removes a directory.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "rename")
            {
                Console.WriteLine("rename   Renames a file.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "type")
            {
                Console.WriteLine("type   Displays the contents of a text file.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "import")
            {
                Console.WriteLine("import   import text file(s) from your computer.");
            }

            else if (CommandArray2Arg[0].ToLower() == "help" && CommandArray2Arg[1].ToLower() == "export")
            {
                Console.WriteLine("export   export text file(s) to your computer.");
            }

            else if (CommandArray2Arg[0].ToLower() == "md")
            {
                if (Program.CurrentDirectory.Search(CommandArray2Arg[1]) == -1)
                {
                    DirectoryEntry d = new DirectoryEntry(CommandArray2Arg[1].ToCharArray(), 1 , 0, 0);
                    Program.CurrentDirectory.DirectoryTable.Add(d);
                    Program.CurrentDirectory.Write_Directory();
                    //  Program.CurrentDirectory.ReadDirectory();

                    if (Program.CurrentDirectory.Parent != null)
                    {
                        Program.CurrentDirectory.Parent.UpdateContent(Program.CurrentDirectory.GetDirectoryEntry());
                        Program.CurrentDirectory.Parent.Write_Directory();
                        // Program.CurrentDirectory.ReadDirectory();
                    }
                }
                else
                {
                    Console.WriteLine("Folder Exist");
                }
            }

            else if (CommandArray2Arg[0].ToLower() == "dir"/*for subdirectories*/)
            {
                int count = 0;

                string[] pathAFtersplit = CommandArray2Arg[1].Split("\\");
                for (int i = 0; i < pathAFtersplit.Length; i++)
                {
                    string last = "";

                    for (int j = 0; j < pathAFtersplit[i].Length; j++)
                    {
                        last += pathAFtersplit[i][j];
                    }

                    for (int x = last.Length; x < 11; x++)
                    {
                        last += " ";
                    }

                    for (int x = pathAFtersplit[0].Length; x < 11; x++)
                    {
                        pathAFtersplit[0] += " ";
                    }

                    count++;
                    string root = new string(VirtualDisk.Root.FileName);
                    if (pathAFtersplit[0] == root)
                    {
                        if (count > 0)
                        {
                            int index = Program.CurrentDirectory.Search(last);
                            if (index != -1)
                            {
                                if (Program.CurrentDirectory.DirectoryTable[index].fileAttribute == 0x02)
                                {
                                    Console.WriteLine("it's a file");
                                }
                                else
                                {
                                    if ((pathAFtersplit.Length) - 1 != i)
                                    {
                                        int f_cluster = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                                        Directory sub_dir = new Directory(last.ToCharArray(), 0x01, f_cluster, 0, Program.CurrentDirectory);
                                        //main.Current_Directory.DirectoryTable.Add(sub_dir);
                                        Program.CurrentDirectory = sub_dir;
                                        Program.Path += "\\" + last.Trim();
                                        Program.CurrentDirectory.ReadDirectory();
                                        continue;

                                    }
                                    else
                                    {
                                        int f_cluster = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                                        Directory sub_dir = new Directory(last.ToCharArray(), 0x01, f_cluster, 0, Program.CurrentDirectory);
                                        //main.Current_Directory.DirectoryTable.Add(sub_dir);
                                        Program.CurrentDirectory = sub_dir;
                                        Program.Path += "\\" + last.Trim();
                                        Program.CurrentDirectory.ReadDirectory();
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine("The folder is not exist in this partition");
                        break;
                        
                    }
                    if (i == pathAFtersplit.Length - 1) 
                    {
                        int FileCounter = 0;
                        int FolderCounter = 0;
                        int FileSizes = 0;
                        int free = 0;
                        int length = Program.CurrentDirectory.DirectoryTable.Count;
                        byte att = 0x00;

                        for (int j = 0; j < length; j++)
                        {
                            string f = new string(Program.CurrentDirectory.DirectoryTable[j].FileName);
                            att = Program.CurrentDirectory.DirectoryTable[j].fileAttribute;


                            if (att == 1)  //ده لو هو فولدر
                            {
                                FolderCounter++;
                                Console.WriteLine("             <Dir>     " + f);
                            }
                            else if (att == 0)
                            {

                                FileCounter++;
                                FileSizes = Program.CurrentDirectory.DirectoryTable[j].fileSize;

                                Console.WriteLine("             <File>   " + " " + f);
                            }
                        }
                        free = FatTable.FreeSpaces();
                        Console.WriteLine();
                        Console.WriteLine("  " + FileCounter + " File(s)          " + FileSizes + " bytes");
                        Console.WriteLine("  " + FolderCounter + " Folder(s)        " + free + " free");
                    }
                }
            }

            else if (CommandArray2Arg[0].ToLower() == "rd")
            {
                int index = Program.CurrentDirectory.Search(CommandArray2Arg[1].ToString());
                if (index != -1)
                {
                    int FirstCluster = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                    Directory d = new Directory(CommandArray2Arg[1].ToCharArray(), 1, FirstCluster, 0, Program.CurrentDirectory);
                    d.DeleteDirectory();
                    d.Write_Directory();
                    d.ReadDirectory();
                }
                else
                {
                    Console.WriteLine("Folder not Exist");
                }
            }

            else if (CommandArray2Arg[0].ToLower() == "cd")
            {
                string k = new string(VirtualDisk.Root.FileName);
                string l = new string(CommandArray2Arg[1]);
                var x = CommandArray2Arg[1].Length;
                if (x < 11)
                {
                    for (int i = x; i < 11; i++)
                    {
                        l += " ";

                    }
                }
                var temp = VirtualDisk.Root;
                if (CommandArray2Arg[1] == "..")
                {
                    Program.CurrentDirectory = temp;
                    string back = new string(Program.CurrentDirectory.FileName);
                    Program.Path = back;
                }
                int index = Program.CurrentDirectory.Search(CommandArray2Arg[1]);
                if (index != -1)
                {
                    byte Attribute = Program.CurrentDirectory.DirectoryTable[index].fileAttribute;
                    if (Attribute == 1)
                    {
                        int FirstCluster = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                        Directory d = new Directory(CommandArray2Arg[1].ToLower().ToCharArray(), 1, FirstCluster, 0, Program.CurrentDirectory);
                        Program.CurrentDirectory = d;
                        Program.Path += "\\" + CommandArray2Arg[1].ToString();
                        Program.CurrentDirectory.ReadDirectory();
                    }
                    else
                    {
                        Console.WriteLine("Specified Folder isn't exist , it's File.");
                    }
                }
                else if (l.Length != k.Length)
                {
                    Console.WriteLine("Folder Name isn't Exist");
                }
            }

            else if (CommandArray2Arg[0].ToLower() == "import") //بجيب فايل من الجهاز وبحطه فى الفيرتشوال ديسك
            {
                if (!File.Exists(CommandArray2Arg[1].ToString()))
                {
                    Console.WriteLine("The file is not Exist");
                }
                else
                {
                    string fileName = "";
                    string FileContent = "";
                    int FileSizeN;
                    int LastIndexofBack = (CommandArray2Arg[1].LastIndexOf("\\"));
                    for (int i = LastIndexofBack + 1; i < CommandArray2Arg[1].Length; i++)
                    {
                        fileName += CommandArray2Arg[1][i];
                    }
                    FileContent += File.ReadAllText(CommandArray2Arg[1].ToString());
                    FileSizeN = FileContent.Length;
                    int indexFile = Program.CurrentDirectory.Search(fileName);
                    if (indexFile != -1)
                    {
                        Console.WriteLine("File Already Exist");
                    }
                    else
                    {
                        if (FileSizeN > 0)
                        {
                            int FirstCluster = FatTable.getAvailableBlock();
                            FatTable.setNext(-1, FirstCluster);
                            FileEntryClass f = new FileEntryClass(fileName.ToCharArray(), 0, FirstCluster, FileSizeN, Program.CurrentDirectory, FileContent);
                            f.FileContent = FileContent;
                            f.WriteFileContent();
                            Directory d = new Directory(fileName.ToCharArray(), 0 , FirstCluster, FileSizeN, Program.CurrentDirectory);
                            d.fileSize = FileSizeN;
                            d.firstCluster = FirstCluster;
                            Program.CurrentDirectory.DirectoryTable.Add(d);
                            Program.CurrentDirectory.Write_Directory();
                            Program.CurrentDirectory.ReadDirectory();
                        }
                        else
                        {
                            int FirstCluster = FatTable.getAvailableBlock();
                            FatTable.setNext(-1, FirstCluster);
                            FileEntryClass f = new FileEntryClass(fileName.ToCharArray(), 0, FirstCluster, FileSizeN, Program.CurrentDirectory, FileContent);
                            f.WriteFileContent();
                            DirectoryEntry d = new DirectoryEntry(fileName.ToCharArray(), 0, 0, FileSizeN);
                            Program.CurrentDirectory.DirectoryTable.Add(d);
                            Program.CurrentDirectory.Write_Directory();
                            Program.CurrentDirectory.ReadDirectory();
                        }
                    }
                }
            }

            else if (CommandArray2Arg[0].ToLower() == "type")//display the file content
             {
                 int index = Program.CurrentDirectory.Search(CommandArray2Arg[1].ToString());
                 if (index != -1)
                 {
                     int firstcluster = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                     int filesize = Program.CurrentDirectory.DirectoryTable[index].fileSize;
                     string content = "";
                     FileEntryClass FE = new FileEntryClass(CommandArray2Arg[1].ToCharArray(), 0, firstcluster, filesize, Program.CurrentDirectory, content);
                     FE.ReadFileContent();
                     Console.WriteLine(FE.FileContent);
                 }
                 else
                 {
                     Console.WriteLine("The system cannot find the file specified.");
                 }
             }

            else if (CommandArray2Arg[0].ToLower() == "export")
            {
                int index = Program.CurrentDirectory.Search(CommandArray2Arg[1].ToString());
                if (index == -1)
                {
                    Console.WriteLine("The File is not Exist");
                }
                else
                {
                    if (!System.IO.Directory.Exists(CommandArray2Arg[2].ToString()))
                    {
                        Console.WriteLine("The System Canot find the folder Destination in your computer");
                    }
                    else
                    {
                        if (Program.CurrentDirectory.DirectoryTable[index].fileAttribute == 0)
                        {
                            int FirstCluster = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                            int fileSize = Program.CurrentDirectory.DirectoryTable[index].fileSize;
                            string temp = "";
                            FileEntryClass f = new FileEntryClass(CommandArray2Arg[1].ToCharArray(), 0, FirstCluster, fileSize, Program.CurrentDirectory, temp);
                            f.WriteFileContent();
                            f.ReadFileContent();
                            StreamWriter StreamWriter = new StreamWriter(CommandArray2Arg[2].ToString() + "\\" + CommandArray2Arg[1].ToString());
                            StreamWriter.Write(f.FileContent);
                            StreamWriter.Flush();
                            StreamWriter.Close();
                        }
                        else if (Program.CurrentDirectory.DirectoryTable[index].fileAttribute == 1)
                        {
                            int FirstCluster = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                            int fileSize = Program.CurrentDirectory.DirectoryTable[index].fileSize;
                            Directory f = new Directory(CommandArray2Arg[1].ToCharArray(), 1, FirstCluster, fileSize, Program.CurrentDirectory);
                            f.Write_Directory();
                            f.ReadDirectory();
                            StreamWriter StreamWriter = new StreamWriter(CommandArray2Arg[2].ToString() + "\\" + CommandArray2Arg[1].ToString());
                            StreamWriter.Write(f);
                            StreamWriter.Flush();
                            StreamWriter.Close();
                        }
                    }
                }
            }

            else if (CommandArray2Arg[0].ToLower() == "rename")
            {
                int indexOld = Program.CurrentDirectory.Search(CommandArray2Arg[1].ToString());
                if (indexOld == -1)
                {
                    Console.WriteLine("The File is not Exist");
                }
                else
                {
                    int indexNew = Program.CurrentDirectory.Search(CommandArray2Arg[2].ToString());
                    if (indexNew != -1)
                    {
                        Console.WriteLine("can't Rename");
                    }
                    else
                    {
                        int firstClusterOld = Program.CurrentDirectory.DirectoryTable[indexOld].firstCluster;
                        int FileSize = Program.CurrentDirectory.DirectoryTable[indexOld].fileSize;
                        if (Program.CurrentDirectory.DirectoryTable[indexOld].fileAttribute == 0)
                        {
                            FileEntryClass f = new FileEntryClass(CommandArray2Arg[1].ToCharArray(), 0, firstClusterOld, FileSize, Program.CurrentDirectory, "");
                            f.FileName = CommandArray2Arg[2].ToCharArray();
                            f.WriteFileContent();
                            f.ReadFileContent();
                            Program.CurrentDirectory.Write_Directory();
                        }
                        else
                        {
                            DirectoryEntry d = new DirectoryEntry(CommandArray2Arg[2].ToCharArray(), 1, firstClusterOld, FileSize);
                            Program.CurrentDirectory.DirectoryTable.RemoveAt(indexOld);
                            Program.CurrentDirectory.DirectoryTable.Insert(indexOld, d);
                            Program.CurrentDirectory.Write_Directory();
                        }

                    }
                }
            }

            else if (CommandArray2Arg[0].ToLower() == "del") // for only files
            {
                int index = Program.CurrentDirectory.Search(CommandArray2Arg[1].ToString());
                if (index == -1)
                {
                    Console.WriteLine("The File is not Exist");
                }
                else
                {
                    if (Program.CurrentDirectory.DirectoryTable[index].fileAttribute == 1)
                    {
                        Console.WriteLine("this is a folder");
                    }
                    else
                    {
                        int f_cluster = Program.CurrentDirectory.DirectoryTable[index].firstCluster;
                        int file_size = Program.CurrentDirectory.DirectoryTable[index].fileSize;
                        FileEntryClass f = new FileEntryClass(CommandArray2Arg[1].ToCharArray(), 0, f_cluster, file_size, Program.CurrentDirectory, "");
                        f.DeleteFile(CommandArray2Arg[1].ToString());
                        Program.CurrentDirectory.Write_Directory();
                        Program.CurrentDirectory.ReadDirectory();
                    }
                }
            }

            else if (CommandArray2Arg[0].ToLower() == "copy")// for only files
            {
                int indexSource = Program.CurrentDirectory.Search(CommandArray2Arg[1].ToString());
                if (indexSource == -1)  // السورس مش موجود
                {
                    Console.WriteLine("The File is not Exist");
                }
                else // لقى السورس
                {
                    string fileName = "";

                    fileName = CommandArray2Arg[2].ToString();
                    int destination_index = Program.CurrentDirectory.Search(fileName);
                    if (destination_index != -1)
                    {
                        if (Program.CurrentDirectory.FileName == CommandArray2Arg[2].ToCharArray())
                        {
                            Console.WriteLine(" The main destination and the new one are the same.. please enter another destination");
                        }
                        else
                        {
                            int F_Cluster = Program.CurrentDirectory.DirectoryTable[destination_index].firstCluster;
                            Directory d = new Directory(CommandArray2Arg[2].ToCharArray(), 1 , F_Cluster, 0, Program.CurrentDirectory);

                            int f_cluster = Program.CurrentDirectory.DirectoryTable[indexSource].firstCluster;
                            int file_size = Program.CurrentDirectory.DirectoryTable[indexSource].fileSize;
                            Program.CurrentDirectory = d;

                            Program.Path += "\\" + CommandArray2Arg[2].ToString();
                            FileEntryClass f = new FileEntryClass(CommandArray2Arg[1].ToCharArray(), 0, f_cluster, file_size, Program.CurrentDirectory, "");
                            Program.CurrentDirectory.DirectoryTable.Add(f);
                            Program.CurrentDirectory.Write_Directory();
                            Program.CurrentDirectory.ReadDirectory();
                        }
                    }
                    else
                    {
                        int F_Cluster = FatTable.getAvailableBlock();
                        Directory d = new Directory(CommandArray2Arg[2].ToCharArray(), 0, F_Cluster, 0, Program.CurrentDirectory);
                        Program.CurrentDirectory.DirectoryTable.Add(d);

                        int f_cluster = Program.CurrentDirectory.DirectoryTable[indexSource].firstCluster;
                        int file_size = Program.CurrentDirectory.DirectoryTable[indexSource].fileSize;
                        Program.CurrentDirectory = d;

                        Program.Path += "\\" + CommandArray2Arg[2].ToString();
                        FileEntryClass f = new FileEntryClass(CommandArray2Arg[1].ToCharArray(), 0, f_cluster, file_size, Program.CurrentDirectory, "");
                        Program.CurrentDirectory.DirectoryTable.Add(f);
                        Program.CurrentDirectory.Write_Directory();
                        Program.CurrentDirectory.ReadDirectory();

                    }
                }
            }

            else
            {
                Console.WriteLine(CommandArray2Arg[0] + " is not a valid command.");
                Console.WriteLine("please valid Command ");
            }
        }
    }
}
