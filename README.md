# Folder Synchronization Tool

## Overview

This project implements a simple folder synchronization tool in C#. The tool synchronizes two directories: a 
source folder and a replica folder. The synchronization is one-way, meaning that the contents of the replica 
folder will be updated to match the source folder.

## Features

- **One-Way Synchronization**: Keeps the replica folder identical to the source folder.
- **Periodic Syncing**: Synchronization occurs at specified intervals.
- **Logging**: File operations (creation, copying, and removal) are logged to both the console and a 
specified log file.
- **Command-Line Arguments**: Folder paths, synchronization interval, and log file path are provided via 
command-line arguments.

## Requirements

- .NET SDK (version 5.0 or higher)
- C# knowledge for potential modifications

## Usage

To run the application, use the following command format in the terminal:

```bash
dotnet run <sourceFolderPath> <replicaFolderPath> <syncIntervalInSeconds> <logFilePath>


# Folder Synchronization Application

## Description
This application synchronizes two folders: a source folder and a replica folder. It ensures that the replica 
folder is an exact copy of the source folder.

## Usage
To run the application, use the following command format in the terminal:

```bash
dotnet run <sourceFolderPath> <replicaFolderPath> <syncIntervalInSeconds> <logFilePath>

dotnet run /Users/minjinsorganbaatar/source /Users/minjinsorganbaatar/replica 10 
/Users/minjinsorganbaatar/logfile.txt



