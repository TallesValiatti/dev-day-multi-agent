package com.agents.configuration;

import java.io.InputStream;
import java.util.Properties;

public class AppConfiguration {

    public static Properties load() {
        Properties props = new Properties();
        try (InputStream input = AppConfiguration.class.getClassLoader().getResourceAsStream("config.properties")) {
            props.load(input);
        } catch (Exception e) {
            throw new RuntimeException("Failed to load config.properties", e);
        }
        return props;
    }
}