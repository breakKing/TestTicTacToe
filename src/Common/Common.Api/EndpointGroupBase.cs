using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Common.Api;

/// <summary>
/// Базовый класс группы эндпоинтов
/// </summary>
public abstract class EndpointGroupBase : Group
{
    protected EndpointGroupBase(string groupName, string routePrefix)
    {
        ConfigureBasicParams(groupName, routePrefix);
    }

    /// <summary>
    /// Конфигурация базовых параметров группы эндпоинтов 
    /// </summary>
    /// <param name="groupName">Наименование группы (используется как тэг в сваггере)</param>
    /// <param name="routePrefix">
    /// Префикс пути для всех эндпоинтов группы (добавляется после глобального префикса, указанного в пайплайне приложения)
    /// </param>
    private void ConfigureBasicParams(string groupName, string routePrefix)
    {
        Configure(routePrefix, ep =>
        {
            ep.Tags(groupName);
            ep.Options(o => o
                .WithGroupName(groupName)
                .WithTags(groupName));
        });
    }
}