package com.agents.openapi;

import java.util.Properties;

import com.agents.configuration.AppConfiguration;

import io.javalin.openapi.OpenApiLicense;
import io.javalin.openapi.OpenApiServer;
import io.javalin.openapi.OpenApiServerVariable;
import io.javalin.openapi.plugin.OpenApiPlugin;

public class OpenApiPluginFactory {

    public static OpenApiPlugin createConfig() {
        Properties properties = AppConfiguration.load();
        String port = properties.getProperty("server.port", "5250");
        String hostname = properties.getProperty("server.hostname", "localhost");

        OpenApiLicense openApiLicense = new OpenApiLicense();
        openApiLicense.setName("Apache 2.0");
        openApiLicense.setIdentifier("Apache-2.0");

        OpenApiServerVariable portServerVariable = new OpenApiServerVariable();
        portServerVariable.setDefault(port);
        portServerVariable.setDescription("Port of the server");

        OpenApiServerVariable hostnameServerVariable = new OpenApiServerVariable();
        hostnameServerVariable.setDefault(hostname);
        hostnameServerVariable.setDescription("Hostname of the server");

        OpenApiServer server = new OpenApiServer();
        server.setUrl("https://{hostname}:{port}/");
        server.setDescription("Server hosting Price API");
        server.addVariable("port", portServerVariable);
        server.addVariable("hostname", hostnameServerVariable);

        return new OpenApiPlugin(pluginConfig -> {
            pluginConfig.withDefinitionConfiguration((version, definition) -> {
                definition.withInfo(info -> {
                            info.setTitle("Price API");
                            info.setSummary("API to get prices");
                            info.setDescription("This is the API responsible for getting prices for each product based on their code");
                            info.setLicense(openApiLicense);
                            info.setVersion("1.0.0");
                        })
                        .withServer(server);
            })
            .withDocumentationPath("/OpenAI/v1.json");
        });
    }
}
