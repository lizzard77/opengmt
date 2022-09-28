export async function putObject(url, obj, onError = null)
{
    try
    {
        //console.log("PUT", url, obj);        
        const response = await fetch(url, 
                { 
                    method: "PUT",
                    headers: {
                        'Accept': 'application/json, text/plain',
                        'Content-Type': 'application/json;charset=UTF-8'
                    }, 
                    body: JSON.stringify(obj)
                });
        if (!response.ok) // or check for response.status
            throw new Error(response.statusText);

        const responseData = await response.text();
        if (responseData)
            return JSON.parse(responseData);
    } 
    catch (e)
    {
        if (typeof onError === "function")
            onError(e);
    }   
}

export async function postObject(url, obj, onError = null)
{
    try
    {
        //console.log("PUT", url, obj);        
        const response = await fetch(url, 
                { 
                    method: "POST",
                    headers: {
                        'Accept': 'application/json, text/plain',
                        'Content-Type': 'application/json;charset=UTF-8'
                    }, 
                    body: JSON.stringify(obj)
                });
        if (!response.ok) // or check for response.status
            throw new Error(response.statusText);

        const responseData = await response.text();
        if (responseData)
            return JSON.parse(responseData);
    } 
    catch (e)
    {
        if (typeof onError === "function")
            onError(e);
    }   
}

export async function get(url, onError = null)
{
    try
    {
        const response = await fetch(url);
        if (!response.ok) // or check for response.status
            throw new Error(response.statusText);
        const responseData = await response.json();
        //console.log("GET", url, responseData);
        return responseData;
    } 
    catch (e)
    {
        if (typeof onError === "function")
            onError(e);
    }
}