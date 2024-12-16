import childProcess from 'child_process'

export const spawnProcess = (
  command: string,
  args: string[],
): Promise<Buffer> => {
  return new Promise<Buffer>((resolve, reject) => {
    const process = childProcess.spawn(command, args)

    const output: Buffer[] = []

    process.stdout?.on('data', (data: Buffer) => output.push(data))
    process.stderr?.on('data', (data: Buffer) => output.push(data))

    process.on('error', (error) => {
      reject(new Error(error.message))
    })

    process.on('close', (code) => {
      if (code === 0) {
        resolve(Buffer.concat(output))
      } else {
        reject(
          new Error(
            `The following command failed with exit code ${code}:\n\n${command} ${args.join(' ')}`,
          ),
        )
      }
    })
  })
}
