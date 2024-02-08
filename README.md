# Quote Generator Client-Server System

## Overview

The Quote Generator Client-Server System is a simple desktop application that facilitates the retrieval of motivational quotes. The system comprises two components: a server-side application (QuoteServer) and a client-side application (QuoteClient). The server manages a collection of quotes categorized into themes like "business," "inspiration," and "stress," while the client interacts with the server to request and receive quotes.

## Technologies Used

- C# Programming Language
- .NET Framework
- TCP/IP Communication
- Socket Programming
- Asynchronous Programming (Possible future enhancement)
- Dictionary Data Structure
- Random Number Generation
- Exception Handling
- Console User Interface
- IP Address and Port Configuration
- DateTime Handling
- Encoding and Byte Manipulation

## Getting Started

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/your-username/quote-generator-client-server.git
   ```
1. **Navigate to the Server Application:**
```bash
cd quote-generator-client-server/QuoteServer
```
3. **Compile and run the server:**
```bash
dotnet run
```
The server will start and listen for client connections on IP address 127.0.0.1 and port 8888.

4. **Navigate to the Client Application:**
```bash
cd quote-generator-client-server/QuoteClient
```
## Usage
The server application logs client connections and disconnections, providing information about connected clients and timestamps.
The client application prompts users to input a category (e.g., "business," "inspiration," "stress") or "DISCONNECT" to end the connection.
Upon requesting a quote, the server responds with a random quote from the specified category or an error message.
The client displays the received quote or error message.
## Contributing
Feel free to contribute to the project by forking the repository and creating a pull request. If you encounter any issues or have suggestions, please open an issue on the GitHub repository.
