namespace SharpQuery
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public static class Attributes
    {
        public static object? attr(this Control elementData, string attributeName)
        {
            PropertyInfo? propertyInfo = elementData.GetType().GetProperty(attributeName);
            if (propertyInfo != null)
            {
                object? propertyValue = propertyInfo.GetValue(elementData);
                if (propertyValue == null)
                {
                    return string.Empty;
                }
                return propertyValue;
            }

            return string.Empty; // Если атрибут не найден
        }

        public static void css(this Control elementData, string attributeName, string attributeValue)
        {
            PropertyInfo? propertyInfo = elementData.GetType().GetProperty(attributeName);
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                if (propertyInfo.PropertyType == typeof(Font))
                {
                    FontConverter fontConverter = new();
                    Font? font = (Font?)fontConverter.ConvertFromString(attributeValue);
                    propertyInfo.SetValue(elementData, font);
                }
                else
                {
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                    object? convertedValue = typeConverter.ConvertFromString(attributeValue);
                    propertyInfo.SetValue(elementData, convertedValue);
                }
            }
        }

        public static void css(this Control elementData, Dictionary<string, string> attributes)
        {
            foreach (var attribute in attributes)
            {
                string attributeName = attribute.Key;
                string attributeValue = attribute.Value;

                elementData.css(attributeName, attributeValue);
            }
        }

        public static int height(this Control elementData)
        {
            return elementData.Height;
        }

        public static int offset(this Control elementData)
        {
            return elementData.Margin.Horizontal + elementData.Margin.Vertical;
        }

        public static int offsetbottom(this Control elementData)
        {
            return elementData.Margin.Bottom;
        }

        public static int offsethorizontal(this Control elementData)
        {
            return elementData.Margin.Horizontal;
        }

        public static int offsetleft(this Control elementData)
        {
            return elementData.Margin.Left;
        }

        public static int offsetright(this Control elementData)
        {
            return elementData.Margin.Right;
        }

        public static int offsettop(this Control elementData)
        {
            return elementData.Margin.Top;
        }

        public static int offsetvertical(this Control elementData)
        {
            return elementData.Margin.Vertical;
        }

        public static Point position(this Control elementData)
        {
            return elementData.Location;
        }

        public static string val(this Control elementData)
        {
            return elementData.Text;
        }

        public static int width(this Control elementData)
        {
            return elementData.Width;
        }
    }

    public static class Console
    {
        public class Error
        {
            private readonly string errorMessage;

            public Error(string message)
            {
                errorMessage = message;
            }

            public string getMessage()
            {
                return errorMessage;
            }
        }

        public static void log(params dynamic[]? strings)
        {
            Debug.WriteLine(string.Join(" ", strings));
        }

        public static void error(string message)
        {
            Error e = new(message);
            Debug.WriteLine($"Error: {e.getMessage()}");
            Debugger.Break();
        }

        public static void dir()
        {
            Debug.WriteLine(Application.ExecutablePath);
        }
    }

    public static class Effects
    {
        public static async Task sleep(dynamic timeout)
        {
            int time = 0;
            switch (timeout)
            {
                case "fast":
                    time = 200;
                    break;
                case "medium":
                    time = 400;
                    break;
                case "slow":
                    time = 600;
                    break;
                default:
                    if (timeout is not int)
                    {
                        throw new ArgumentException($"Неверный аргумент, переданный в {nameof(timeout)}");
                    }
                    else
                    {
                        time = timeout;
                    }
                    break;
            }
            await Task.Delay(time);
        }
    }

    public static class EventHandlers
    {
        public class ControlEventArgs : EventArgs
        {
            public Control Target { get; set; }

            public ControlEventArgs(Control target)
            {
                Target = target;
            }
        }

        public static void bind(this Control eventData, string eventType, Action handler)
        {
            Dictionary<string, Action> eventMappings = new()
        {
            { "blur", () => eventData.blur(handler) },
            { "changelocation", () => eventData.changelocation(handler) },
            { "changeparent", () => eventData.changeparent(handler) },
            { "changesize", () => eventData.changesize(handler) },
            { "changetext", () => eventData.changetext(handler) },
            { "click", () => eventData.click(handler) },
            { "dblclick", () => eventData.dblclick(handler) },
            { "focus", () => eventData.focus(handler) },
            { "hover", () => eventData.hover(handler) },
            { "keydown", () => eventData.keydown(handler) },
            { "keypress", () => eventData.keypress(handler) },
            { "keyup", () => eventData.keyup(handler) },
            { "mousedown", () => eventData.mousedown(handler) },
            { "mouseenter", () => eventData.mouseenter(handler) },
            { "mouseleave", () => eventData.mouseleave(handler) },
            { "mousemove", () => eventData.mousemove(handler) },
            { "mouseup", () => eventData.mouseup(handler) },
            { "ready", () => eventData.ready(handler) },
            { "resize", () => eventData.resize(handler) },
            { "scroll", () => eventData.scroll(handler) }
        };

            if (eventMappings.ContainsKey(eventType))
            {
                eventMappings[eventType].Invoke();
            }
            else
            {
                throw new ArgumentException("Unsupported event type");
            }
        }

        public static void bind(this Control eventData, string eventType, Action<ControlEventArgs> handler)
        {
            Dictionary<string, Action> eventMappings = new()
        {
            { "blur", () => eventData.blur(handler) },
            { "changelocation", () => eventData.changelocation(handler) },
            { "changeparent", () => eventData.changeparent(handler) },
            { "changesize", () => eventData.changesize(handler) },
            { "changetext", () => eventData.changetext(handler) },
            { "click", () => eventData.click(handler) },
            { "dblclick", () => eventData.dblclick(handler) },
            { "focus", () => eventData.focus(handler) },
            { "hover", () => eventData.hover(handler) },
            { "keydown", () => eventData.keydown(handler) },
            { "keypress", () => eventData.keypress(handler) },
            { "keyup", () => eventData.keyup(handler) },
            { "mousedown", () => eventData.mousedown(handler) },
            { "mouseenter", () => eventData.mouseenter(handler) },
            { "mouseleave", () => eventData.mouseleave(handler) },
            { "mousemove", () => eventData.mousemove(handler) },
            { "mouseup", () => eventData.mouseup(handler) },
            { "ready", () => eventData.ready(handler) },
            { "resize", () => eventData.resize(handler) },
            { "scroll", () => eventData.scroll(handler) }
        };

            if (eventMappings.ContainsKey(eventType))
            {
                eventMappings[eventType].Invoke();
            }
            else
            {
                throw new ArgumentException("Unsupported event type");
            }
        }

        public static void blur(this Control eventData, Action handler)
        {
            eventData.LostFocus += (sender, e) => handler();
        }

        public static void blur(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.LostFocus += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void changelocation(this Control eventData, Action handler)
        {
            eventData.LocationChanged += (sender, e) => handler();
        }

        public static void changelocation(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.LocationChanged += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void changeparent(this Control eventData, Action handler)
        {
            eventData.ParentChanged += (sender, e) => handler();
        }

        public static void changeparent(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.ParentChanged += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void changesize(this Control eventData, Action handler)
        {
            eventData.SizeChanged += (sender, e) => handler();
        }

        public static void changesize(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.SizeChanged += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void changetext(this Control eventData, Action handler)
        {
            eventData.TextChanged += (sender, e) => handler();
        }

        public static void changetext(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.TextChanged += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void click(this Control eventData, Action handler)
        {
            eventData.Click += (sender, e) => handler();
        }

        public static void click(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.Click += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void dblclick(this Control eventData, Action handler)
        {
            eventData.DoubleClick += (sender, e) => handler();
        }

        public static void dblclick(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.DoubleClick += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void focus(this Control eventData, Action handler)
        {
            eventData.GotFocus += (sender, e) => handler();
        }

        public static void focus(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.GotFocus += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void hover(this Control eventData, Action handler)
        {
            eventData.MouseHover += (sender, e) => handler();
        }

        public static void hover(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.MouseHover += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void keydown(this Control eventData, Action handler)
        {
            eventData.KeyDown += (sender, e) => handler();
        }

        public static void keydown(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.KeyDown += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void keypress(this Control eventData, Action handler)
        {
            eventData.KeyPress += (sender, e) => handler();
        }

        public static void keypress(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.KeyPress += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void keyup(this Control eventData, Action handler)
        {
            eventData.KeyUp += (sender, e) => handler();
        }

        public static void keyup(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.KeyUp += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
                handler(args);
            };
        }

        public static void mousedown(this Control eventData, Action handler)
        {
            eventData.MouseDown += (sender, e) => handler();
        }

        public static void mousedown(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.MouseDown += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
            };
        }

        public static void mouseenter(this Control eventData, Action handler)
        {
            eventData.MouseEnter += (sender, e) => handler();
        }

        public static void mouseenter(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.MouseEnter += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
            };
        }

        public static void mouseleave(this Control eventData, Action handler)
        {
            eventData.MouseLeave += (sender, e) => handler();
        }

        public static void mouseleave(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.MouseLeave += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
            };
        }

        public static void mousemove(this Control eventData, Action handler)
        {
            eventData.MouseMove += (sender, e) => handler();
        }

        public static void mousemove(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.MouseMove += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
            };
        }

        public static void mouseup(this Control eventData, Action handler)
        {
            eventData.MouseUp += (sender, e) => handler();
        }

        public static void mouseup(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.MouseUp += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
            };
        }

        public static void on(this Control eventData, string eventTypes, Action handler)
        {
            Dictionary<string, Action> eventMappings = new()
        {
            { "blur", () => eventData.blur(handler) },
            { "changelocation", () => eventData.changelocation(handler) },
            { "changeparent", () => eventData.changeparent(handler) },
            { "changesize", () => eventData.changesize(handler) },
            { "changetext", () => eventData.changetext(handler) },
            { "click", () => eventData.click(handler) },
            { "dblclick", () => eventData.dblclick(handler) },
            { "focus", () => eventData.focus(handler) },
            { "hover", () => eventData.hover(handler) },
            { "keydown", () => eventData.keydown(handler) },
            { "keypress", () => eventData.keypress(handler) },
            { "keyup", () => eventData.keyup(handler) },
            { "mousedown", () => eventData.mousedown(handler) },
            { "mouseenter", () => eventData.mouseenter(handler) },
            { "mouseleave", () => eventData.mouseleave(handler) },
            { "mousemove", () => eventData.mousemove(handler) },
            { "mouseup", () => eventData.mouseup(handler) },
            { "ready", () => eventData.ready(handler) },
            { "resize", () => eventData.resize(handler) },
            { "scroll", () => eventData.scroll(handler) }
        };

            string[] eventTypeArray = eventTypes.Split(' ');

            foreach (string eventType in eventTypeArray)
            {
                if (eventMappings.ContainsKey(eventType))
                {
                    eventMappings[eventType].Invoke();
                }
                else
                {
                    throw new ArgumentException("Unsupported event type");
                }
            }
        }

        public static void on(this Control eventData, string eventTypes, Action<ControlEventArgs> handler)
        {
            Dictionary<string, Action> eventMappings = new()
        {
            { "blur", () => eventData.blur(handler) },
            { "changelocation", () => eventData.changelocation(handler) },
            { "changeparent", () => eventData.changeparent(handler) },
            { "changesize", () => eventData.changesize(handler) },
            { "changetext", () => eventData.changetext(handler) },
            { "click", () => eventData.click(handler) },
            { "dblclick", () => eventData.dblclick(handler) },
            { "focus", () => eventData.focus(handler) },
            { "hover", () => eventData.hover(handler) },
            { "keydown", () => eventData.keydown(handler) },
            { "keypress", () => eventData.keypress(handler) },
            { "keyup", () => eventData.keyup(handler) },
            { "mousedown", () => eventData.mousedown(handler) },
            { "mouseenter", () => eventData.mouseenter(handler) },
            { "mouseleave", () => eventData.mouseleave(handler) },
            { "mousemove", () => eventData.mousemove(handler) },
            { "mouseup", () => eventData.mouseup(handler) },
            { "ready", () => eventData.ready(handler) },
            { "resize", () => eventData.resize(handler) },
            { "scroll", () => eventData.scroll(handler) }
        };

            string[] eventTypeArray = eventTypes.Split(' ');

            foreach (string eventType in eventTypeArray)
            {
                if (eventMappings.ContainsKey(eventType))
                {
                    eventMappings[eventType].Invoke();
                }
                else
                {
                    throw new ArgumentException("Unsupported event type");
                }
            }
        }

        public static void ready(this Control eventData, Action handler)
        {
            handler();
        }

        public static void ready(this Control eventData, Action<ControlEventArgs> handler)
        {
            ControlEventArgs args = new(eventData)
            {
                Target = eventData
            };
            handler(args);
        }

        public static void resize(this Control eventData, Action handler)
        {
            eventData.Resize += (sender, e) => handler();
        }

        public static void resize(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.Resize += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
            };
        }

        public static void scroll(this Control eventData, Action handler)
        {
            eventData.MouseWheel += (sender, e) => handler();
        }

        public static void scroll(this Control eventData, Action<ControlEventArgs> handler)
        {
            eventData.MouseWheel += (sender, e) =>
            {
                ControlEventArgs args = new(eventData)
                {
                    Target = eventData
                };
            };
        }
    }
}