package com.agents.model;

import java.util.List;
import java.util.UUID;

import com.agents.error.NotFoundException;

public class Prices {

    private static final List<Price> prices = List.of(
        new Price(UUID.randomUUID().toString(), "WH001", "12.45" ),
        new Price(UUID.randomUUID().toString(), "CM002", "30.50" ),
        new Price(UUID.randomUUID().toString(), "BS003", "21.40" ),
        new Price(UUID.randomUUID().toString(), "NS004", "99.10" ),
        new Price(UUID.randomUUID().toString(), "WB005", "10.50" ),
        new Price(UUID.randomUUID().toString(), "DL006", "22.40" ),
        new Price(UUID.randomUUID().toString(), "PC007", "15.55")
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
