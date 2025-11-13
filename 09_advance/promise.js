

 const promiseOne= new Promise(function(resolve, reject){

    setTimeout(function(){
        console.log("Async task is complete");
        resolve()
        // console.log("Hellow");
        
    },1000)
 })

 promiseOne.then(function(){
    console.log("Promise Consumed");
    
 })

/////////

new Promise(function (resolve,reject) {
    setTimeout( function(){
        console.log("Async task 2");
        
    }, 1000)
}).then(function(){
    console.log("Async task 2 completed");
    
})

///////////

const promiseThree= new Promise(function (resolve, reject) {
    setTimeout(() => {
        console.log("Async 3");
        resolve({username: "Ab", email : "ab@exmaple.com"})
        
    }, 1000);
})

promiseThree.then(function(user) {
    console.log(user);
    
})

//////////


const promiseFour= new Promise( function(resolve, reject){
    setTimeout(() => {
        let error = true;
        if(!error){
            resolve({username:"User",pass:"123"})
        }else{
            reject('Something went wrong')
        }
    }, 1000);
})

promiseFour
.then((user) =>{
    console.log(user);
    return user.username
}).then((username) =>{
    console.log(username);
}).catch(function (error) {
    console.log(error);
}).finally(() => console.log("Finally The promise is either resolved or rejected"))

const promisefive = new Promise(function(resolve,reject){
    setTimeout(() => {
        let error = true;
        if(!error){
            resolve({username: "javascript", password: "123"})
        }else{
            reject('ERROR: JS went wrong')
        }
    }, 1000);
})

async function consumePromiseFive() {
    try {
        const response = await promisefive
        console.log(response);
    } catch (error) {
        console.log(error);   
    }
}

consumePromiseFive()

//////

// async function getAllUsers() {
//     try {
//         const response = await fetch('https://jsonplaceholder.typicode.com/users')
//         const data = await response.json()
//         console.log(data);
//     } catch (error) {
//         console.log("E: ", error);
        
//     }
// }



// getAllUsers()

/////

fetch("https://jsonplaceholder.typicode.com/users")
.then((response) => {
    return response.json()
})
.then((data) => {
    console.log(data );
})
.catch((error) => console.log(error)
)