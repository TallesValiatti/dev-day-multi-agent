namespace MultiAgent.Api.Services;

public static class Constants
{
    public const string ModelSupportDoc = """
                                       # Azure AI Agent Service – Model & Region Guide

                                       Agents can tap into a rich catalogue of models, each with distinct capabilities and prices. Some features (such as tools or function calls) require the latest generation. Below you’ll find a quick at-a-glance reference in **beautiful Markdown**.

                                       ---

                                       ## 1. Deployment Options for Azure OpenAI

                                       | Deployment type | Key traits | Ideal when you… |
                                       |-----------------|------------|-----------------|
                                       | **Standard** | *Global routing* for higher throughput.<br>*No capacity reservation*—pay only for what you use. | - Need elastic scale.<br>- Don’t mind shared capacity. |
                                       | **Provisioned (PTU)** | *Reserved throughput units* (PTUs) locked to you.<br>Predictable billing & performance. | - Have steady, high-volume traffic.<br>- Need strict latency guarantees. |

                                       > 📚 **More details:** See the [deployment types guide](#) and the [Provisioned Throughput Unit documentation](#).

                                       ---

                                       ## 2. Supported Azure OpenAI Models by Region (Pay-as-you-go)

                                       | Region | gpt-4o<br>2024-11-20 | gpt-4o<br>2024-05-13 | gpt-4o<br>2024-08-06 | gpt-4o-mini<br>2024-07-18 | gpt-4<br>0613 | gpt-4<br>1106-Preview | gpt-4<br>0125-Preview | gpt-4-turbo<br>2024-04-09 | gpt-4-32k<br>0613 | gpt-35-turbo<br>0613 | gpt-35-turbo<br>1106 | gpt-35-turbo<br>0125 | gpt-35-turbo-16k<br>0613 |
                                       |--------|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|
                                       | **australiaeast** | – | – | – | – | ✅ | ✅ | – | – | ✅ | ✅ | ✅ | ✅ | ✅ |
                                       | **eastus** | – | ✅ | ✅ | ✅ | – | – | ✅ | ✅ | – | ✅ | – | ✅ | ✅ |
                                       | **eastus2** | – | ✅ | ✅ | ✅ | – | ✅ | – | ✅ | – | ✅ | – | ✅ | ✅ |
                                       | **francecentral** | – | – | – | – | ✅ | ✅ | – | – | ✅ | ✅ | ✅ | – | ✅ |
                                       | **japaneast** | ✅ | – | – | – | – | – | – | – | – | ✅ | – | ✅ | ✅ |
                                       | **norwayeast** | – | – | – | – | – | ✅ | – | – | – | – | – | – | – |
                                       | **polandcentral** | – | – | – | – | – | – | – | – | – | – | – | – | – |
                                       | **southindia** | – | – | – | – | – | ✅ | – | – | – | – | ✅ | ✅ | – |
                                       | **swedencentral** | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | – | ✅ | ✅ | ✅ | ✅ | – | ✅ |
                                       | **switzerlandnorth** | – | – | – | – | ✅ | – | – | – | ✅ | – | – | ✅ | ✅ |
                                       | **uaenorth** | – | – | – | – | – | – | – | – | – | – | – | – | – |
                                       | **uksouth** | – | – | – | – | ✅ | ✅ | – | – | ✅ | ✅ | ✅ | ✅ | ✅ |
                                       | **westus** | – | ✅ | ✅ | ✅ | – | ✅ | – | ✅ | – | – | ✅ | ✅ | – |
                                       | **westus3** | – | ✅ | ✅ | ✅ | – | ✅ | – | ✅ | – | – | – | ✅ | – |

                                       Legend: **✅ = Available** | **– = Not available**

                                       ---

                                       ## 3. Non-Microsoft Models (Azure AI Foundry)

                                       | Model | Family |
                                       |-------|--------|
                                       | **Meta-Llama-405B-Instruct** | Meta |
                                       | **Cohere-command-r-plus** | Cohere |
                                       | **Cohere-command-r** | Cohere |

                                       ### Quick-start (Python)

                                       ```python
                                       agent = project_client.agents.create_agent(
                                           model="llama-3",
                                           name="my-agent",
                                           instructions="You are a helpful agent."
                                       )
                                       ```

                                       1. Deploy the model in the **Azure AI Foundry portal**.  
                                       2. Use the **deployment name** (e.g., `"llama-3"`) in your agent code.
                                       """;

    public const string FileSearchDoc = """
                                       # Azure AI Agent Service File Search Tool

                                       File search augments agents with knowledge from outside its model, such as proprietary product information or documents provided by your users.  

                                       > **Note**  
                                       > Using the Standard agent setup, the improved file search tool ensures your files remain in your own storage, and your Azure AI Search resource is used to ingest them, ensuring you maintain complete control over your data.

                                       ---

                                       ## File sources

                                       * **Upload local files**  
                                       * **Azure Blob Storage**

                                       ---

                                       ## Usage support

                                       | Azure AI Foundry support | Python SDK | C# SDK | JavaScript SDK | REST API | Basic agent setup | Standard agent setup |
                                       |-------------------------|:----------:|:------:|:--------------:|:--------:|:-----------------:|:--------------------:|
                                       | ✔️ | ✔️ | ✔️ | ✔️ | ✔️ | File upload only | File upload **and** using BYO blob storage |

                                       ---

                                       ## Dependency on agent setup

                                       ### Basic agent setup

                                       * The file search tool has the same functionality as Azure OpenAI Assistants.  
                                       * **Microsoft-managed search and storage resources are used.**
                                         * Uploaded files get stored in Microsoft-managed storage.  
                                         * A vector store is created using a Microsoft-managed search resource.  

                                       ### Standard agent setup

                                       * The file search tool uses the Azure AI Search and Azure Blob Storage resources you connected during agent setup.
                                         * Uploaded files get stored in **your** connected Azure Blob Storage account.  
                                         * Vector stores get created using **your** connected Azure AI Search resource.  

                                       ---

                                       For both agent setups, Azure OpenAI handles the entire ingestion process, which includes:

                                       * Automatically parsing and chunking documents  
                                       * Generating and storing embeddings  
                                       * Utilizing both vector and keyword searches to retrieve relevant content for user queries  

                                       > There is no difference in the code between the two setups; the only variation is in **where your files and created vector stores are stored.**

                                       ---

                                       ## How it works

                                       The file search tool implements several retrieval best practices out of the box to help you extract the right data from your files and augment the model’s responses. The file search tool:

                                       1. **Rewrites user queries** to optimize them for search.  
                                       2. **Breaks down complex user queries** into multiple searches it can run in parallel.  
                                       3. **Runs both keyword and semantic searches** across both agent and thread vector stores.  
                                       4. **Reranks search results** to pick the most relevant ones before generating the final response.  

                                       **Default settings**

                                       | Setting | Value |
                                       |---------|-------|
                                       | Chunk size | 800 tokens |
                                       | Chunk overlap | 400 tokens |
                                       | Embedding model | `text-embedding-3-large` at 256 dimensions |
                                       | Maximum number of chunks added to context | 20 |

                                       ---

                                       ## Vector stores

                                       Vector store objects give the file search tool the ability to search your files. Adding a file to a vector store automatically parses, chunks, embeds, and stores the file in a vector database that's capable of both keyword and semantic search.

                                       * Each vector store can hold **up to 10,000 files**.  
                                       * Vector stores can be attached to **both agents and threads**.  
                                       * Currently you can attach **at most one vector store to an agent** and **at most one vector store to a thread**.  

                                       You can remove these files from a vector store by either:

                                       * **Deleting the vector store file object**, or  
                                       * **Deleting the underlying file object**, which removes the file from all `vector_store` and `code_interpreter` configurations across all agents and threads in your organization.  

                                       **Limits**

                                       * Maximum file size: **512 MB**  
                                       * Each file should contain **no more than 5,000,000 tokens** (computed automatically when you attach a file).

                                       ---

                                       ## Ensuring vector store readiness before creating runs

                                       We highly recommend that you ensure all files in a `vector_store` are fully processed before you create a run. This ensures that all the data in your vector store is searchable.

                                       * Check readiness by using the **polling helpers** in the SDKs, or by manually polling the vector store object to ensure the status is `completed`.  
                                       * As a fallback, there’s a **60-second maximum wait** in the run object when the thread’s vector store contains files that are still being processed.  
                                         * This ensures that any files your users upload in a thread are fully searchable before the run proceeds.  
                                         * The fallback wait **does not apply** to the agent’s vector store.
                                       """;

    public const string SalesOpenApiSchema = """
                                             {
                                               "openapi": "3.0.1",
                                               "info": {
                                                 "title": "OverviewAzureAiAgentService.SalesApi | v1",
                                                 "version": "1.0.0"
                                               },
                                               "servers": [
                                                 {
                                                   "url": "<URL>"
                                                 }
                                               ],
                                               "paths": {
                                                 "/sales": {
                                                   "get": {
                                                     "tags": [
                                                       "OverviewAzureAiAgentService.SalesApi"
                                                     ],
                                                     "operationId": "GetSales",
                                                     "parameters": [
                                                       {
                                                         "name": "startDate",
                                                         "in": "query",
                                                         "schema": {
                                                           "type": "string",
                                                           "format": "date-time"
                                                         }
                                                       },
                                                       {
                                                         "name": "endDate",
                                                         "in": "query",
                                                         "schema": {
                                                           "type": "string",
                                                           "format": "date-time"
                                                         }
                                                       }
                                                     ],
                                                     "responses": {
                                                       "200": {
                                                         "description": "OK",
                                                         "content": {
                                                           "application/json": {
                                                             "schema": {
                                                               "type": "array",
                                                               "items": {
                                                                 "$ref": "#/components/schemas/Sale"
                                                               }
                                                             }
                                                           }
                                                         }
                                                       }
                                                     }
                                                   }
                                                 }
                                               },
                                               "components": {
                                                 "schemas": {
                                                   "Sale": {
                                                     "type": "object",
                                                     "properties": {
                                                       "id": {
                                                         "type": "string",
                                                         "format": "uuid"
                                                       },
                                                       "customerEmail": {
                                                         "type": "string"
                                                       },
                                                       "createdAt": {
                                                         "type": "string",
                                                         "format": "date-time"
                                                       },
                                                       "totalValue": {
                                                         "type": "number",
                                                         "format": "double"
                                                       }
                                                     }
                                                   }
                                                 }
                                               },
                                               "tags": [
                                                 {
                                                   "name": "OverviewAzureAiAgentService.SalesApi"
                                                 }
                                               ]
                                             }
                                             """;
}