var io = require('socket.io')(process.env.PORT || 3000);

console.log("server started");

var playerCount = 0;
var Randing = 0;
Randing = Math.floor(Math.random() * 99); //Random Number
io.on("connection",function(socket){

console.log("client connected");

playerCount++;
 
names={
name:playerCount
}
socket.emit("open",names);
socket.on("go",function(num){
 
     console.log("Player = "+num.num);
    if(num.num == Randing){
        socket.emit("go");
        
        socket.broadcast.emit('over',{data:num.name});
        Randing = Math.floor(Math.random() * 99);
    
    }else if(num.num <= Randing){
        
        socket.emit("GetBack",{data:"To Low"});

    }else if(num.num >= Randing){
        
        socket.emit("GetBack",{data:"To High"});

    }
     
  console.log ("ServerNum = "+Randing);
     
}); 
socket.on("over",function(){
 
}); 
socket.on("GetBack",function(){
 
}); 
socket.on("disconnect",function(){

    console.log("client disconnect");
    playerCount--;
}); 



});