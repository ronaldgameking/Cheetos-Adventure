using System;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Security.Cryptography;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;


public class GoogleDocs : EditorWindow
{
    private const string RequestUri = "https://discord.com/api/webhooks/852819205773590528/ptsliEMd7fAeJ7PexSx8z1BAJu0aLIS9mBIK3P7eOx_YjqFG_e3ZdNvPQIIQo7Bsu_6D";
    //private const string RequestUri = "https://discord.com/api/webhooks/852819205773590528/ptsliEMd7fAeJ7PexSx8z1BAJu0aLIS9mBIK3P7eOx_YjqFG_e3ZdNvPQIIQo7Bsu_6D";
    const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    static HttpClient client = new HttpClient();
    const string clientId = "261465145096-t04eacbef30cvlgutm3b43bfnmcavlpd.apps.googleusercontent.com";

    [MenuItem("Tools/Google/Auth")]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async static void Authorize()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        EditorUtility.DisplayProgressBar("Processing...", "Begin Job", 0);
        var request = new HttpRequestMessage(new HttpMethod("GET"), AuthorizationEndpoint);

        // Generates state and PKCE values.
        string state = GenerateRandomDataBase64url(32);
        string codeVerifier = GenerateRandomDataBase64url(32);
        string codeChallenge = Base64UrlEncodeNoPadding(Sha256Ascii(codeVerifier));
        const string codeChallengeMethod = "S256";
        
        string redirectUri = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";

        var listener = new HttpListener();
        listener.Prefixes.Add(redirectUri);
        listener.Start();
        EditorUtility.DisplayProgressBar("Listening...", "Google Auth", 33);

        string authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
                AuthorizationEndpoint,
                Uri.EscapeDataString(redirectUri),
                clientId,
                state,
                codeChallenge,
                codeChallengeMethod);

        System.Diagnostics.Process.Start(authorizationRequest);
        var context = await listener.GetContextAsync();
        EditorUtility.DisplayProgressBar("Reading...", "Google Auth", 67);

        var response = context.Response;
        string responseString = "<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>";
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        var responseOutput = response.OutputStream;
        await responseOutput.WriteAsync(buffer, 0, buffer.Length);
        responseOutput.Close();
        listener.Stop();

        // Checks for errors.
        string error = context.Request.QueryString.Get("error");
        if (error is object)
        {
            Debug.Log($"OAuth authorization error: {error}.");
            return;
        }
        if (context.Request.QueryString.Get("code") is null
            || context.Request.QueryString.Get("state") is null)
        {
            Debug.Log($"Malformed authorization response. {context.Request.QueryString}");
            return;
        }

        // extracts the code
        var code = context.Request.QueryString.Get("code");
        var incomingState = context.Request.QueryString.Get("state");

        // Compares the receieved state to the expected value, to ensure that
        // this app made the request which resulted in authorization.
        if (incomingState != state)
        {
            Debug.Log($"Received request with invalid state ({incomingState})");
            return;
        }
        Debug.Log("Authorization code: " + code);
        EditorUtility.ClearProgressBar();
    }
    [MenuItem("Tools/Discord")]
    public async static void AShit()
    {
        //New request
        var request = new HttpRequestMessage(new HttpMethod("POST"), RequestUri);
        //Set message
        request.Content = new StringContent("{\"content\": \"Hello there I can see you <@!287848412999581696>\"}");
        //Only accept JSON as response
        request.Headers.TryAddWithoutValidation("Accept", "application/json");
        //Tell the webhook that what we are sending is JSON
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        //Send request
        var response = await client.SendAsync(request);
        //Print result in unity console (empty on success)
        Debug.Log(await response.Content.ReadAsStringAsync());
    }
    [MenuItem("Tools/Discord (Insult)")]
    public async static void AShitHereWeGo()
    {

        //New request
        var request = new HttpRequestMessage(new HttpMethod("POST"), RequestUri);
        var requestInsult = new HttpRequestMessage(new HttpMethod("GET"), "https://evilinsult.com/generate_insult.php?lang=en&type=text");
        var responseInsult = await client.SendAsync(requestInsult);
        string insult = Uri.UnescapeDataString(responseInsult.Content.ReadAsStringAsync().Result.Replace("&quot;", "\""));
        
        System.Threading.Thread.Sleep(100);
        //Set message
        request.Content = new StringContent(string.Format("{{\"content\": \"Hello there I can see you <@!287848412999581696> Your insult is now: {0}\"}}", Uri.EscapeDataString(insult)));
        //Only accept JSON as response
        request.Headers.TryAddWithoutValidation("Accept", "application/json");
        //Tell the webhook that what we are sending is JSON
        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        //Send request
        var response = await client.SendAsync(request);
        Debug.Log(string.Format("{{\"content\": \"Hello there I can see you <@!287848412999581696>\" Your insult is now: {0}}}", insult));
        Debug.Log(await response.Content.ReadAsStringAsync());
        responseInsult.Dispose();
        requestInsult.Dispose();
        response.Dispose();
        request.Dispose();
        //Print result in unity console (empty on success)
    }

    public static int GetRandomUnusedPort()
    {
        TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }

    /// <summary>
    /// Returns URI-safe data with a given input length.
    /// </summary>
    /// <param name="length">Input length (nb. output will be longer)</param>
    /// <returns></returns>
    private static string GenerateRandomDataBase64url(uint length)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] bytes = new byte[length];
        rng.GetBytes(bytes);
        return Base64UrlEncodeNoPadding(bytes);
    }

    /// <summary>
    /// Returns the SHA256 hash of the input string, which is assumed to be ASCII.
    /// </summary>
    private static byte[] Sha256Ascii(string text)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(text);
        using (SHA256Managed sha256 = new SHA256Managed())
        {
            return sha256.ComputeHash(bytes);
        }
    }

    /// <summary>
    /// Base64url no-padding encodes the given input buffer.
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    private static string Base64UrlEncodeNoPadding(byte[] buffer)
    {
        string base64 = Convert.ToBase64String(buffer);

        // Converts base64 to base64url.
        base64 = base64.Replace("+", "-");
        base64 = base64.Replace("/", "_");
        // Strips padding.
        base64 = base64.Replace("=", "");

        return base64;
    }
}
