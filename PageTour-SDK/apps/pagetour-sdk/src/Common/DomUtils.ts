export class DomUtils {
  public static appendTo = (source: HTMLElement, html: string): HTMLElement => {
    if (!source || !html) {
      return source;
    }
    const newElement = DomUtils.htmlToElement(html);
    source.appendChild(newElement);
    return newElement;
  }

  public static appendToBody = (html: string): HTMLElement => {
    const body = document.getElementsByTagName('body')[0];
    return DomUtils.appendTo(body, html);
  }

  public static removeFromDom = (element: HTMLElement): void => {
    if (element) {
      const parent = element.parentElement;
      if (parent) {
        parent.removeChild(element);
      }
    }
  }

  public static show = (element: HTMLElement): HTMLElement => {
    if (!element) {
      return element;
    }

    element.style.display = 'block'
    return element;
  }

  private static htmlToElement = (html: string): HTMLElement => {
    const template = document.createElement('template');
    html = html.trim(); // Never return a text node of whitespace as the result
    template.innerHTML = html;
    return template.content.firstChild as HTMLElement;
  }
}