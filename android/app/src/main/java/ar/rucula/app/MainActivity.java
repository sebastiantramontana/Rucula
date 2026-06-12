package ar.rucula.app;

import android.os.Bundle;
import android.webkit.WebSettings;
import android.webkit.WebView;

import com.getcapacitor.BridgeActivity;

public class MainActivity extends BridgeActivity {

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        WebView webView = getBridge().getWebView();
        WebSettings settings = webView.getSettings();

        String ua = settings.getUserAgentString();

        ua = ua.replace("; wv", "")
               .replace(" Version/4.0", "");

        settings.setUserAgentString(ua);
    }
}