let imgEle= document.querySelector('img');

const requestUrl= "https://api.github.com/users/hiteshchoudhary";

const xhr= new XMLHttpRequest();

xhr.open('GET', requestUrl);

xhr.onreadystatechange = function(){
    console.log(xhr.readyState);
    if(xhr.readyState === 4){
        const data = JSON.parse(this.responseText)
        imgEle.src= `${data.avatar_url}`
        document.querySelector('h4').innerHTML= `${data.name}`
        document.querySelector('p').innerHTML= `${data.followers}`
    }
}

xhr.send();