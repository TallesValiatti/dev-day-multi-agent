package com.agents;

import java.util.Properties;

import com.agents.configuration.AppConfiguration;
import com.agents.controller.PriceController;
import com.agents.openapi.OpenApiPluginFactory;

import io.javalin.Javalin;
import static io.javalin.apibuilder.ApiBuilder.get;
import static io.javalin.apibuilder.ApiBuilder.path;
import io.javalin.openapi.plugin.OpenApiPlugin;

public class App {
    public static void main(String[] args) {
        Properties properties = AppConfiguration.load();
        int port = Integer.parseInt(properties.getProperty("server.port", "5250"));

        OpenApiPlugin plugin = OpenApiPluginFactory.createConfig();
 
        Javalin.create(config -> {
            config.registerPlugin(plugin);
            config.router.apiBuilder(() -> {
                path("price", () -> {
                    path("{code}", () -> {
                        get(PriceController::getPrice);
                    });
                });
            });
        }).start(port);

    }


}