package com.agents.controller;

import com.agents.error.ErrorResponse;
import com.agents.error.NotFoundException;
import com.agents.model.Price;
import com.agents.model.Prices;

import io.javalin.http.Context;
import io.javalin.openapi.HttpMethod;
import io.javalin.openapi.OpenApi;
import io.javalin.openapi.OpenApiContent;
import io.javalin.openapi.OpenApiParam;
import io.javalin.openapi.OpenApiResponse;

public class PriceController {

    @OpenApi(
        summary = "Get price by code",
        operationId = "getPriceByCode",
        path = "/price/{code}",
        methods = HttpMethod.GET,
        pathParams = {@OpenApiParam(name = "code", type = String.class, description = "The product code", required = true)},
        tags = {"Price"},
        responses = {
            @OpenApiResponse(status = "200", content = {@OpenApiContent(from = Price.class)}),
            @OpenApiResponse(status = "404", content = {@OpenApiContent(from = ErrorResponse.class)})
        }
    )
    public static void getPrice(Context ctx) {
        String code = ctx.pathParam("code");
        try {
            Price price = Prices.getPriceFromCode(code);
            ctx.json(price);
            
        } catch (NotFoundException exception) {
            ctx.status(404).json(new ErrorResponse("Cannot find price for code " + code));
        }
    }
}