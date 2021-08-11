using GerenciamentoComercio_API.v1.Controllers;
using GerenciamentoComercio_Domain.DTOs.Clients;
using GerenciamentoComercio_Domain.Utils.APIMessage;
using GerenciamentoComercio_Domain.Utils.IUserApp;
using GerenciamentoComercio_Domain.Utils.ModelStateValidation;
using GerenciamentoComercio_Domain.v1.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

[Authorize]
[Route("v{version:apiVersion}/clients/")]
[ApiVersion("1.0")]
public class ClientsController : MainController
{
    private readonly IClientsServices _clientsServices;

    public ClientsController(IClientsServices clientsServices,
        IUserApp userApp, INotifier notifier) : base(userApp, notifier)
    {
        _clientsServices = clientsServices;
    }

    [HttpGet]
    [SwaggerOperation("Returns all clients")]
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(List<GetClientsResponse>))]
    public async Task<IActionResult> GetAllClientsAsync()
    {
        APIMessage response = await _clientsServices.GetAllClientsAsync();

        return StatusCode((int)response.StatusCode, response.ContentObj);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Returns a client by id")]
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(GetClientByIdResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Client not found", typeof(string))]
    public async Task<IActionResult> GetClientById(int id)
    {
        APIMessage response = await _clientsServices.GetClientById(id);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return StatusCode((int)response.StatusCode, response.Content);
        }

        return StatusCode((int)response.StatusCode, response.ContentObj);
    }

    [HttpPost]
    [SwaggerOperation("Add a new client")]
    [SwaggerResponse(StatusCodes.Status200OK, "Client created successfully", typeof(string))]
    public IActionResult AddNewCient(AddNewClientRequest request)
    {
        if (!ModelState.IsValid) return CustomReturn(ModelState);

        APIMessage response = _clientsServices.AddNewClient(request, UserName);

        return StatusCode((int)response.StatusCode, response.Content);
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Updates a client")]
    [SwaggerResponse(StatusCodes.Status200OK, "Client updated successfully", typeof(string))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Client not found", typeof(string))]
    public async Task<IActionResult> UpdateClientAsync(UpdateClientRequest request, int id)
    {
        if (!ModelState.IsValid) return CustomReturn(ModelState);

        APIMessage response = await _clientsServices.UpdateClientAsync(request, id);

        return StatusCode((int)response.StatusCode, response.Content);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Deletes a client")]
    [SwaggerResponse(StatusCodes.Status200OK, "Client deleted successfully", typeof(string))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Client not found", typeof(string))]
    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        APIMessage response = await _clientsServices.DeleteClientAsync(id);

        return StatusCode((int)response.StatusCode, response.Content);
    }
}
