protoc --csharp_out=./output/csharp ./src/login.proto
protoc --csharp_out=./output/csharp ./src/fight.proto

protoc --go_out=./output/go ./src/login.proto
protoc --go_out=./output/go ./src/fight.proto