export async function putObject(url, obj)
{
    //console.log("PUT", url, obj);        
    return await fetch(url, 
            { 
                method: "PUT",
                headers: {
                    'Accept': 'application/json, text/plain',
                    'Content-Type': 'application/json;charset=UTF-8'
                }, 
                body: JSON.stringify(obj)
            });
}

export async function get(url, obj)
{
    const responseData = await fetch(url).then(r => r.json());
    //console.log("GET", url, responseData);
    return responseData;
}