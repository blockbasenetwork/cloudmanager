function fetchRequesterStake(account) {
    return fetch("http://localhost:8000/api/Requester/CheckRequesterConfig").then(res => res.json())
        .then(resp => resp.currencyBalance[0]).catch(e => console.log(e));
}

//"currencyBalance": [
//"89899.8700 BBT"
//    ]