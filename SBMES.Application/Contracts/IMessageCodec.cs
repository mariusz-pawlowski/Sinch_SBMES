using SBMES.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBMES.Application.Contracts
{
    internal interface IMessageCodec
    {
        byte[] Encode(Message message);
        Message Decode(byte[] data);

    }
}
