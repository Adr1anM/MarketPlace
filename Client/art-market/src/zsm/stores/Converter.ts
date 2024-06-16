
export function base64ToFile(base64: string, mimeType: string) {
    try {
      let byteString;
      if (base64.startsWith('data:image')) {
        byteString = atob(base64.split(',')[1]);
      } else {
        byteString = atob(base64);
      }
  
      const ab = new ArrayBuffer(byteString.length);
      const ia = new Uint8Array(ab);
  
      for (let i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
      }
  
      const blob = new Blob([ab], { type: mimeType });
      const extension = mimeType.split('/')[1];
      const fileName = `file_${Date.now()}.${extension}`;
      return new File([blob], fileName, { type: mimeType });
    } catch (error) {
      console.error('Error converting base64 to file:', error);
      throw error;
    }
  }
  