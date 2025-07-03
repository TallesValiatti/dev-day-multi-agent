package com.agents.model;

import java.util.List;
import java.util.UUID;

import com.agents.error.NotFoundException;

public class Prices {

    private static final List<Price> prices = List.of(
        new Price(UUID.randomUUID().toString(), "WH001", "Wireless Headphones" ),
        new Price(UUID.randomUUID().toString(), "CM002", "Coffee Mug" ),
        new Price(UUID.randomUUID().toString(), "BS003", "Bluetooth Speaker" ),
        new Price(UUID.randomUUID().toString(), "NS004", "Notebook Set" ),
        new Price(UUID.randomUUID().toString(), "WB005", "Water Bottle" ),
        new Price(UUID.randomUUID().toString(), "DL006", "Desk Lamp" ),
        new Price(UUID.randomUUID().toString(), "PC007", "Phone Case")
        );


    public static Price getPriceFromCode(String code) {
        for (Price price : prices) {
            if (price.code().equalsIgnoreCase(code)) {
                return price;
            }
        }
        throw new NotFoundException("Unable to find price for code " + code);
    }

}
